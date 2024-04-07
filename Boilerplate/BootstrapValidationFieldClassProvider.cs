using Microsoft.AspNetCore.Components.Forms;

namespace MegaSync.Boilerplate;

internal sealed class BootstrapValidationFieldClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        var isValid = editContext.IsValid(fieldIdentifier);
        return isValid ? "" : "is-invalid";
    }
}