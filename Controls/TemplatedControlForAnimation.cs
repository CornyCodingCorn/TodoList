using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;

namespace ToDoList.Controls;

[PseudoClasses(":animating")]
public class TemplatedControlForAnimation : TemplatedControl
{
    // Used to stop animation from playing at the start
    protected void StartAnimating()
    {
        PseudoClasses.Set(":animating", true);
    }
}