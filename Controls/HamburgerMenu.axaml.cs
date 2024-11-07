using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.Controls;

public partial class Item : ObservableObject
{
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _imgUrl = string.Empty;
    [ObservableProperty] private Type _type = typeof(object);
}

public partial class HamburgerMenu : TemplatedControl
{
    public static readonly DirectProperty<HamburgerMenu, ICommand> ExpandMenuCommandProperty =
        AvaloniaProperty.RegisterDirect<HamburgerMenu, ICommand>(nameof(ExpandMenuCommand), o => o.ExpandMenuCommand);
    
    public static readonly StyledProperty<bool> IsExpandedProperty = AvaloniaProperty.Register<HamburgerMenu, bool>(
        nameof(IsExpanded));

    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly StyledProperty<double> HamburgerMenuButtonSizeProperty = AvaloniaProperty.Register<HamburgerMenu, double>(
        nameof(HamburgerMenuButtonSize), defaultValue: 28d);

    public double HamburgerMenuButtonSize
    {
        get => GetValue(HamburgerMenuButtonSizeProperty);
        set => SetValue(HamburgerMenuButtonSizeProperty, value);
    }

    public static readonly StyledProperty<double> HeaderHeightProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(nameof(HeaderHeight), defaultValue: 40d);

    public double HeaderHeight
    {
        get => GetValue(HeaderHeightProperty);
        set => SetValue(HeaderHeightProperty, value);
    }

    public static readonly StyledProperty<FontFamily> HeaderFontFamilyProperty =
        AvaloniaProperty.Register<HamburgerMenu, FontFamily>(
            nameof(HeaderFontFamily), defaultValue: FontFamily.Default);

    public FontFamily HeaderFontFamily
    {
        get => GetValue(HeaderFontFamilyProperty);
        set => SetValue(HeaderFontFamilyProperty, value);
    }

    public static readonly StyledProperty<double> HeaderFontSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(HeaderFontSize), defaultValue: 18d);

    public double HeaderFontSize
    {
        get => GetValue(HeaderFontSizeProperty);
        set => SetValue(HeaderFontSizeProperty, value);
    }

    public static readonly StyledProperty<double> NormalWidthProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(nameof(NormalWidth), defaultValue: 48d);

    public double NormalWidth
    {
        get => GetValue(NormalWidthProperty);
        set => SetValue(NormalWidthProperty, value);
    }

    public static readonly StyledProperty<double> ExpandableWidthProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(nameof(ExpandableWidth), defaultValue: 180d);

    public double ExpandableWidth
    {
        get => GetValue(ExpandableWidthProperty);
        set => SetValue(ExpandableWidthProperty, value);
    }

    public static readonly StyledProperty<double> BodyItemHeightProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(BodyItemHeight));

    public double BodyItemHeight
    {
        get => GetValue(BodyItemHeightProperty);
        set => SetValue(BodyItemHeightProperty, value);
    }

    public static readonly StyledProperty<double> BodyFontSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(BodyFontSize));

    public double BodyFontSize
    {
        get => GetValue(BodyFontSizeProperty);
        set => SetValue(BodyFontSizeProperty, value);
    }

    public static readonly StyledProperty<IEnumerable<Item>> BodyItemsSourceProperty =
        AvaloniaProperty.Register<HamburgerMenu, IEnumerable<Item>>(
            nameof(BodyItemsSource));

    public IEnumerable<Item> BodyItemsSource
    {
        get => GetValue(BodyItemsSourceProperty);
        set => SetValue(BodyItemsSourceProperty, value);
    }

    public static readonly StyledProperty<double> FooterItemHeightProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(FooterItemHeight));

    public double FooterItemHeight
    {
        get => GetValue(FooterItemHeightProperty);
        set => SetValue(FooterItemHeightProperty, value);
    }

    public static readonly StyledProperty<double> FooterFontSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(FooterFontSize));

    public double FooterFontSize
    {
        get => GetValue(FooterFontSizeProperty);
        set => SetValue(FooterFontSizeProperty, value);
    }

    public static readonly StyledProperty<IEnumerable<Item>> FooterItemsSourceProperty =
        AvaloniaProperty.Register<HamburgerMenu, IEnumerable<Item>>(
            nameof(FooterItemsSource));

    public IEnumerable<Item> FooterItemsSource
    {
        get => GetValue(FooterItemsSourceProperty);
        set => SetValue(FooterItemsSourceProperty, value);
    }

    [RelayCommand]
    private void ExpandMenu()
    {
        IsExpanded = !IsExpanded;
    }
}