namespace Langueedu.Sdk
{
  internal static class Configs
  {
    /// <summary>
    /// Defines the DefaultEndpoint
    /// </summary>
    public static string DefaultEndpoint = "http://localhost:57679";

    /// <summary>
    /// Defines the _baseGatewayShoppingEndpoint
    /// </summary>
    private static string _baseGatewayShoppingEndpoint;

    /// <summary>
    /// Defines the _baseIdentityEndpoint
    /// </summary>
    private static string _baseIdentityEndpoint;


    /// <summary>
    /// Initializes a new instance of the <see cref="Configs"/> class.
    /// </summary>
    static Configs()
    {
      BaseIdentityEndpoint = DefaultEndpoint;
      BaseGatewayEndpoint = DefaultEndpoint;
    }


    /// <summary>
    /// Gets or sets the AuthorizeEndpoint
    /// </summary>
    public static string AuthorizeEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the BaseGatewayCategoryEndpoint
    /// </summary>
    public static string BaseGatewayEndpoint
    {
      get { return _baseGatewayShoppingEndpoint; }
      set
      {
        _baseGatewayShoppingEndpoint = value;
        UpdateGatewayEndpoint(_baseGatewayShoppingEndpoint);
      }
    }

    /// <summary>
    /// Gets or sets the BaseIdentityEndpoint
    /// </summary>
    public static string BaseIdentityEndpoint
    {
      get { return _baseIdentityEndpoint; }
      set
      {
        _baseIdentityEndpoint = value;
        UpdateEndpoint(_baseIdentityEndpoint);
      }
    }

    /// <summary>
    /// Gets the ClientId
    /// </summary>
    public static string ClientId = "blazor";

    /// <summary>
    /// Gets the ClientId
    /// </summary>
    public static string ClientSecret = "Development Secret Key";

    /// <summary>
    /// Gets or sets the GatewaEndpoint
    /// </summary>
    public static string GatewaEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the LogoutEndpoint
    /// </summary>
    public static string LogoutEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the Scope
    /// </summary>
    public static string Scope = "langueedu_api langueedu_web offline_access openid profile";

    /// <summary>
    /// Gets or sets the TokenEndpoint
    /// </summary>
    public static string TokenEndpoint { get; set; }

    /// <summary>
    /// The UpdateEndpoint
    /// </summary>
    /// <param name="endpoint">The endpoint <see cref="string"/></param>
    private static void UpdateEndpoint(string endpoint)
    {
      var connectBaseEndpoint = $"{endpoint}/connect";
      AuthorizeEndpoint = $"{connectBaseEndpoint}/authorize";
      TokenEndpoint = $"{connectBaseEndpoint}/token";
      LogoutEndpoint = $"{connectBaseEndpoint}/endsession";
    }

    private static void UpdateGatewayEndpoint(string endpoint)
    {
      GatewaEndpoint = $"{endpoint}/api";
    }

  }
}
