namespace MauiSqlite
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _localDbService;
        private int _editCustomerId;

        public MainPage(LocalDbService localDbService)
        {
            InitializeComponent();
            _localDbService = localDbService;
            Task.Run(async () => listView.ItemsSource = await _localDbService.GetCustomers());
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if(_editCustomerId == 0)
            {
                await _localDbService.Create(new Customer
                {
                    CustomerName = nameEntryField.Text,
                    Email = emailEntryField.Text,
                    Mobile = mobileEntryField.Text
                });
            }
            else
            {
                await _localDbService.Update(new Customer
                {
                    Id = _editCustomerId,
                    CustomerName = nameEntryField.Text,
                    Email = emailEntryField.Text,
                    Mobile = mobileEntryField.Text
                });

                _editCustomerId = 0;
            }

            nameEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;
            mobileEntryField.Text = string.Empty;

            listView.ItemsSource = await _localDbService.GetCustomers();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var customer = (Customer)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch(action)
            {
                case "Edit":
                    _editCustomerId = customer.Id;
                    nameEntryField.Text = customer.CustomerName;
                    mobileEntryField.Text = customer.Mobile;
                    emailEntryField.Text = customer.Email;
                    break;
                case "Delete":
                    await _localDbService.Delete(customer);
                    listView.ItemsSource = await _localDbService.GetCustomers();
                    break;
            }

        }
    }

}
