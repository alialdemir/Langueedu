using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components
{
    public partial class LeTextField : UIComponentBase
    {
        [Parameter]
        public Expression<Func<string>> ValidationFor { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public InputTypes InputType { get; set; }

        private string _value;

        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
    }
}

