using System.Windows.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Langueedu.Web.Components;

public partial class LeEditForm : UIComponentBase
{
    [Parameter]
    public ICommand ValidSubmitCommand { get; set; }

    [Parameter]
    public object Model { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    internal void ValidFormSubmitted(EditContext editContext)
    {
        ValidSubmitCommand?.Execute(editContext);
    }
}