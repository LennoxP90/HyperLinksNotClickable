using System.Collections.ObjectModel;

namespace HyperLinksNotClickable;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    Hyperlinks.ItemsSource = links;
  }

  private ObservableCollection<HyperLink> links = new ObservableCollection<HyperLink>();

	private void OnClicked(object sender, EventArgs e)
	{
    //Create a hyper link and place it into the collectionview
    string url = "https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/open-browser?tabs=ios";

    FormattedString fs = new FormattedString();
    fs.Spans.Add(
      new HyperlinkSpan()
      {
        Text = url,
        Url = url,
        TextColor = Colors.Blue,
      } );

    HyperLink link = new HyperLink();
    link.Text = fs;

    links.Add( link );
	}
}

public class HyperLink
{
  public FormattedString Text { get; set; }
}

public class HyperlinkSpan : Span
{
  public static readonly BindableProperty UrlProperty =
    BindableProperty.Create( nameof( Url ),
                             typeof( string ),
                             typeof( HyperlinkSpan ),
                             null );

  public string Url
  {
    get { return (string)GetValue( UrlProperty ); }
    set { SetValue( UrlProperty, value ); }
  }

  public HyperlinkSpan()
  {
    GestureRecognizers.Add( new TapGestureRecognizer
    {
      Command = new Command( async () => await Browser.OpenAsync( Url, BrowserLaunchMode.SystemPreferred ) )
    } );
  }
}