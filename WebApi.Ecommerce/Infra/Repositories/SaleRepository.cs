using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Sale;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class SaleRepository : EntityRepository<Sale>, ISaleRepository
    {
        private readonly WebApiDataContext _context;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;

        public SaleRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository
            , IPaymentTypeRepository paymentTypeRepository
            , IPaymentStatusRepository paymentStatusRepository) : base(context, logErroRepository)
        {
            _context = context;
            _paymentTypeRepository = paymentTypeRepository;
            _paymentStatusRepository = paymentStatusRepository;
        }

        public async Task<Sale> TransactionSale(SaleCreateCommand command)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var paymentType = await _paymentTypeRepository.GetByIdAsync(command.PaymentTypeId);
                var paymentStatus = await _paymentStatusRepository.Get(t => t.PaymentStatusId == 1);

                var saleProducts = command.Products.ConvertAll(c => new SaleProduct(
                                                                                        amount: c.Amount,
                                                                                        quantity: c.Quantity,
                                                                                        sale: c.Sale,
                                                                                        productId: c.ProductId
                                                                                    ));

                var sale = new Sale(
                                       creditCard: command.CreditCard,
                                       verificationCode: command.VerificationCode,
                                       validityMonth: command.ValidityMonth,
                                       validityYear: command.ValidityYear,
                                       creditCardName: command.CreditCardName,
                                       customerId: command.CustomerId,
                                       paymentTypeId: paymentType.Id,
                                       paymentStatusId: paymentStatus.Id,
                                       saleProducts: saleProducts
                                   );

                _context.Add(sale);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return sale;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                var erroCompleto = ex;

                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }

            return new Sale();
        }

        public async Task<BootstrapTablePaginationDTO<SalePaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, SaleGetPaginationCommand command)
        {
            var query = $@"
                            SELECT s.id
	                            , s.createdat
	                            , s.updatedat
	                            , s.active
	                            , s.transaction
	                            , c.id as customerid
	                            , c.name
                                , c.document
	                            , c.email 
	                            , ps.description as paymentstatus 
	                            , pt.description as paymenttype
	                            , count(*) over() as Total
                            FROM public.""Sales"" s
                                inner join ""PaymentStatus"" ps on(ps.id = s.paymentstatusid)
                                inner join ""PaymentTypes"" pt on(pt.id = s.paymenttypeid)
                                inner join ""SaleProducts"" sp on(sp.saleid = s.id)
                                inner join ""Products"" p on p.id = sp.productid
                                inner join ""Customers"" c on c.id = s.customerid
                            WHERE 1 = 1
                          ";

            if(command.InitialDate is not null && command.FinalDate is not null)
            {
                query += $@"    AND DATE_TRUNC('DAY', s.createdat) BETWEEN '{command.InitialDate}' and '{command.FinalDate}'";
            }

            if (command.Active is not null)
            {
                query += $@"    AND s.active = {command.Active}";
            }

            if(command.ProductId is not null)
            {
                query += $@"    AND p.id = {command.ProductId}";
            }

            if (command.PaymentStatusId is not null)
            {
                query += $@"    AND p.paymentstatusid = {command.PaymentStatusId}";
            }

            if (command.PaymentTypeId is not null)
            {
                query += $@"    AND p.paymenttypeid = {command.PaymentTypeId}";
            }

            if (command.Document is not null)
            {
                query += $@"    AND c.document = {command.Document}";
            }

            if (!string.IsNullOrEmpty(command.SearchParameter))
            {
                query += $@"
                                AND (
		                            p.description LIKE '%{command.SearchParameter}%'
		                            or p.sku LIKE '%{command.SearchParameter}%'
		                            or c.neighborhood LIKE '%{command.SearchParameter}%'
		                            or c.city LIKE '%{command.SearchParameter}%'
		                            or c.state LIKE '%{command.SearchParameter}%'
	                            )
                           ";
            }

            if (string.IsNullOrEmpty(filter.Order))
            {
                filter.Order = "DESC";
            }

            if (string.IsNullOrEmpty(filter.Sort))
            {
                filter.Sort = "s.createdat";
            }

            return await QueryPaginatedAsync<SalePaginatedDTO>(query, filter);
        }
    }
}
