using System.Windows.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Langueedu.Web.Components;

public abstract class LeButtonBase : UIComponentBase
{
    /// <summary>
    /// The HTML element that will be rendered in the root by the component
    /// By default, is a button
    /// </summary>
    [Parameter]
    public string HtmlTag { get; set; } = "button";

    /// <summary>
    /// The button Type (Button, Submit, Refresh)
    /// </summary>
    [Parameter]
    public ButtonTypes Type { get; set; }

    /// <summary>
    /// If set to a URL, clicking the button will open the referenced document. Use Target to specify where
    /// </summary>
    [Parameter]
    public string Link { get; set; }

    /// <summary>
    /// The target attribute specifies where to open the link, if Link is specified. Possible values: _blank | _self | _parent | _top | <i>framename</i>
    /// </summary>
    [Parameter]
    public string Target { get; set; }

    /// <summary>
    /// If true, the button will be disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Command executed when the user clicks on an element.
    /// </summary>
    [Parameter]
    public ICommand Command { get; set; }

    /// <summary>
    /// Command parameter.
    /// </summary>
    [Parameter]
    public object CommandParameter { get; set; }

    /// <summary>
    /// Button click event.
    /// </summary>
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    protected async Task OnClickHandler(MouseEventArgs ev)
    {
        if (Disabled)
            return;

        await OnClick.InvokeAsync(ev);

        if (Command?.CanExecute(CommandParameter) ?? false)
        {
            Command.Execute(CommandParameter);
        }
    }

    protected override void OnInitialized()
    {
        SetDefaultValues();
    }

    protected override void OnParametersSet()
    {
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        if (Disabled)
        {
            HtmlTag = "button";
            Link = null;
            Target = null;
            return;
        }

        if (!string.IsNullOrWhiteSpace(Link))
        {
            HtmlTag = "a";
        }
    }

    protected ElementReference _elementReference;

    public ValueTask FocusAsync() => _elementReference.FocusAsync();
}
