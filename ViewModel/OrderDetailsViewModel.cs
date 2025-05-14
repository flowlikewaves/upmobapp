using Mobappg4v2.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mobappg4v2.ViewModel
{
    [QueryProperty(nameof(Order), "Order")]
    public class OrderDetailsViewModel : BaseViewModel
    {
        private SellerOrderModel _order;
        private bool _isLoaded;
        private ObservableCollection<TrackOrderModel> _trackData;

        public SellerOrderModel Order
        {
            get => _order;
            set
            {
                SetProperty(ref _order, value);
                _ = LoadOrderDetailsAsync();
            }
        }

        public bool IsLoaded
        {
            get => _isLoaded;
            set => SetProperty(ref _isLoaded, value);
        }

        public ObservableCollection<TrackOrderModel> TrackData
        {
            get => _trackData;
            set => SetProperty(ref _trackData, value);
        }

        public bool HasNotes => !string.IsNullOrWhiteSpace(Order?.Notes);

        public ICommand UpdateStatusCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand BackCommand { get; }

        public OrderDetailsViewModel()
        {
            Title = "Order Details";
            TrackData = new ObservableCollection<TrackOrderModel>();
            BackCommand = new Command(async () => await GoBack());
            UpdateStatusCommand = new Command<string>(async (status) => await UpdateStatus(status));
            AddNoteCommand = new Command(async () => await AddNote());
        }

        private async Task LoadOrderDetailsAsync()
        {
            if (Order != null)
            {
                IsLoaded = false;
                try
                {
                    Title = $"Order #{Order.OrderId}";
                    await LoadTrackingData();
                }
                finally
                {
                    IsLoaded = true;
                }
            }
        }

        private async Task LoadTrackingData()
        {
            // Simulate loading tracking data
            await Task.Delay(500);

            if (Order != null)
            {
                TrackData.Clear();
                var trackList = new List<TrackOrderModel.Track>();
                var track = new TrackOrderModel.Track
                {
                    OrderId = Order.OrderId,
                    Price = Order.TotalAmount.ToString("C"),
                    Status = Order.Status,
                    Images = Order.Items.Select(item => new TrackOrderModel.ImageList 
                    { 
                        ImageUrl = item.ProductImage 
                    }).ToList()
                };
                trackList.Add(track);

                TrackData.Add(new TrackOrderModel(Order.OrderDate.ToString("MMM dd, yyyy"), trackList));
            }
        }

        private async Task UpdateStatus(string status)
        {
            if (Order != null)
            {
                Order.Status = status;
                if (status == "Shipped" && !Order.ShippedDate.HasValue)
                {
                    Order.ShippedDate = DateTime.Now;
                }
                else if (status == "Delivered" && !Order.DeliveredDate.HasValue)
                {
                    Order.DeliveredDate = DateTime.Now;
                }

                // TODO: Update status in database
                await Shell.Current.DisplayAlert("Success", "Order status updated successfully", "OK");
            }
        }

        private async Task AddNote()
        {
            string note = await Shell.Current.DisplayPromptAsync(
                "Add Note",
                "Enter a note for this order:",
                "Save",
                "Cancel",
                "Type your note here...",
                -1,
                Keyboard.Text,
                Order?.Notes);

            if (!string.IsNullOrWhiteSpace(note))
            {
                Order.Notes = note;
                OnPropertyChanged(nameof(HasNotes));
                // TODO: Save note to database
                await Shell.Current.DisplayAlert("Success", "Note added successfully", "OK");
            }
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
