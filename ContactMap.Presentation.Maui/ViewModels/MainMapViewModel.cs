using System.Collections.ObjectModel;
using System.Windows.Input;
using ContactMap.Domain.Entities;
using System.Net.Http.Json;

namespace ContactMap.Presentation.Maui.ViewModels;

/// <summary>
/// ViewModel for the main map page, handling people search and relationship requests.
/// </summary>
public class MainMapViewModel : BindableObject
{
    /// <summary>
    /// The collection of people displayed on the map.
    /// </summary>
    public ObservableCollection<Person> People { get; set; } = [];
    /// <summary>
    /// The search text for filtering people.
    /// </summary>
    public string SearchText { get; set; } = string.Empty;
    /// <summary>
    /// The command to search for people.
    /// </summary>
    public ICommand SearchCommand { get; }
    /// <summary>
    /// The command to request a relationship.
    /// </summary>
    public ICommand RequestRelationshipCommand { get; }

    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://localhost:7135/api"; // Dynamically set this to your API base URL

    public MainMapViewModel()
    {
        _httpClient = new HttpClient();
        SearchCommand = new Command(async () => await OnSearch());
        RequestRelationshipCommand = new Command<Person>(async (person) => await OnRequestRelationship(person));
    }

    private async Task OnSearch()
    {
        try
        {
            List<Person>? people = await _httpClient.GetFromJsonAsync<List<Person>>($"{ApiBaseUrl}/people/search?name={SearchText}");
            People.Clear();
            if (people != null)
            {
                foreach (Person p in people)
                    People.Add(p);
            }
        }
        catch (Exception)
        {
            // TODO: Handle error (log, show message, etc.)
            throw;
        }
    }

    private async Task OnRequestRelationship(Person person)
    {
        try
        {
            // For demo, assume current user is the first in the list (replace with real user logic)
            Guid requesterId = People.FirstOrDefault()?.Id ?? Guid.Empty;
            Guid addresseeId = person.Id;
            if (requesterId == Guid.Empty || addresseeId == Guid.Empty || requesterId == addresseeId)
                return;
            string url = $"{ApiBaseUrl}/people/request-relationship?requesterId={requesterId}&addresseeId={addresseeId}";
            HttpResponseMessage response = await _httpClient.PostAsync(url, null);
            // Optionally handle response
        }
        catch (Exception)
        {
            // TODO: Handle error (log, show message, etc.)
            throw;
        }
    }
}
