using Light.GuardClauses;

namespace DiAndMvvm
{
    public partial class ContactsMasterDetailView
    {
        public ContactsMasterDetailView()
        {
            InitializeComponent();
        }

        public ContactsMasterDetailView(ContactsMasterDetailViewModel viewModel) : this()
        {
            DataContext = viewModel.MustNotBeNull();
        }
    }
}