using System;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Logging;
using Avalonia.Metadata;

namespace ToDoList.Controls;

/// <summary>
/// A costly window that trigger class change event
/// </summary>
[PseudoClasses(":lg", ":md", ":sm", "xl", "xxl", "xs")]
public class ResponsivePanel : TemplatedControl
{
    public static readonly StyledProperty<object?> ContentProperty = AvaloniaProperty.Register<ResponsivePanel, object?>(
        nameof(Content));

    [Content]
    [DependsOn(nameof(ContentTemplate))]
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly StyledProperty<IDataTemplate?> ContentTemplateProperty = AvaloniaProperty.Register<ResponsivePanel, IDataTemplate?>(
        nameof(ContentTemplate));

    public IDataTemplate? ContentTemplate
    {
        get => GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty = AvaloniaProperty.Register<ResponsivePanel, HorizontalAlignment>(
        nameof(HorizontalContentAlignment));

    public HorizontalAlignment HorizontalContentAlignment
    {
        get => GetValue(HorizontalContentAlignmentProperty);
        set => SetValue(HorizontalContentAlignmentProperty, value);
    }

    public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty = AvaloniaProperty.Register<ResponsivePanel, VerticalAlignment>(
        nameof(VerticalContentAlignment));

    public VerticalAlignment VerticalContentAlignment
    {
        get => GetValue(VerticalContentAlignmentProperty);
        set => SetValue(VerticalContentAlignmentProperty, value);
    }
    
    private int _lastSizeBracketIndex = 0;
    private static readonly int[] SizeBrackets = [1400, 1200, 992, 768, 576, -1];
    private static readonly string[] SizeClasses = [":xxl", ":xl", ":lg", ":md", ":sm", ":xs"];

    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        if (e.WidthChanged)
            HandleSizeChanged(e.NewSize);
    }

    private void HandleSizeChanged(Size size)
    {
        for (var i = 0; i < SizeBrackets.Length; i++)
        {
            if (!(SizeBrackets[i] < size.Width)) continue;
            if (_lastSizeBracketIndex == i) break;
            ResetSizePseudoClasses();
            
            var sizeTypeStringBuilder = new StringBuilder(); 
            for (var j = i; j < SizeClasses.Length; j++)
            {
                PseudoClasses.Add(SizeClasses[j]);
                sizeTypeStringBuilder.Append($"{SizeClasses[j]} ");
            }
            Logger.Sink?.Log(LogEventLevel.Information, "Resizing", this, sizeTypeStringBuilder.ToString());
            
            _lastSizeBracketIndex = i;
            break;
        }
    }

    private void ResetSizePseudoClasses()
    {
        foreach (var pseudoClass in SizeClasses)
        {
            PseudoClasses.Remove(pseudoClass);
        }
        PseudoClasses.Add(":xs");
    }
}