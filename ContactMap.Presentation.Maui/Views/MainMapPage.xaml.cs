using ContactMap.Presentation.Maui.ViewModels;
#if WINDOWS
using Maps = CommunityToolkit.Maui.Maps;
#else
using Maps = Microsoft.Maui.Controls.Maps;
#endif
using Microsoft.Maui.Controls.Maps;

namespace ContactMap.Presentation.Maui.Views;

public partial class MainMapPage : ContentPage
{
    public MainMapPage()
    {
        InitializeComponent();
        var vm = new MainMapViewModel();
        BindingContext = vm;
#if WINDOWS

#else
        vm.People.CollectionChanged += async (s, e) => await UpdateMapPinsAsync();
#endif
    }
#if !WINDOWS

    private async Task UpdateMapPinsAsync()
    {
        if (BindingContext is MainMapViewModel vm)
        {
            PeopleMap.Pins.Clear();
            foreach (Domain.Entities.Person person in vm.People)
            {
                Domain.ValueObjects.Address address = person.Address;
                string fullAddress = $"{address.Street}, {address.City}, {address.State}, {address.Country}, {address.PostalCode}";
                try
                {
                    IEnumerable<Location> locations = await Geocoding.GetLocationsAsync(fullAddress);
                    Location? location = locations?.FirstOrDefault();
                    if (location != null)
                    {
                        var pin = new Pin
                        {
                            Label = person.Name,
                            Address = fullAddress,
                            Location = new Location(location.Latitude, location.Longitude)
                        };
                        PeopleMap.Pins.Add(pin);
                    }
                }
                catch
                {
                    // Ignore geocoding errors for now
                }
            }
        }
    }
#endif
}
