using Flunt.Notifications;
using Flunt.Validations;

namespace WebApi.Ecommerce.Domain.Commands
{
    public class FilterCommand : Notifiable<Notification>
    {
        public FilterCommand()
        {
            this.PerPage = 10;
            this.CurrentPage = 1;
        }

        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }

        public string SearchParameter { get; set; }

        public string InitialDate { get; set; }
        public string FinalDate { get; set; }

        public bool? Active { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                   .IsGreaterThan(PerPage, 0, "Registro por página", "A quantidade de registros por página deve ser maior que zero.")
               );

            if(CurrentPage == 0)
            {
                CurrentPage = 1;
            }
        }
    }
}
