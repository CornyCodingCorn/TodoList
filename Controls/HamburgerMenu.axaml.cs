using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.Controls;

public sealed partial class HamburgerMenuItem : ObservableObject, IDisposable
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _iconUrl;
    [ObservableProperty] private Type _type;

    private Bitmap _icon = null!;

    public Bitmap Icon
    {
        get => _icon;
        private set => SetProperty(ref _icon, value);
    }

    public HamburgerMenuItem(string name, string iconUrl, Type type)
    {
        _name = name;
        _type = type;
        _iconUrl = iconUrl;
        LoadImage();
    }

    public void Dispose()
    {
        _icon.Dispose();
    }

    private void LoadImage()
    {
        Icon = new Bitmap(AssetLoader.Open(new Uri(IconUrl)));
    }
}

public partial class HamburgerMenu : TemplatedControl
{
    private const string BodyItemsListName = "TemplateBodyItemsList";
    private const string FooterItemsListName = "TemplateFooterItemsList";

    private ListBox? _bodyList;

    private ListBox? _footerList;

    // Todo: Remove this temp function
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _bodyList = e.NameScope.Find<ListBox>(BodyItemsListName) ??
                    throw new Exception($"Could not find List box with name {BodyItemsListName}");
        _footerList = e.NameScope.Find<ListBox>(FooterItemsListName) ??
                      throw new Exception($"Could not find List box with name {FooterItemsListName}");

        _bodyList.SelectionChanged += ChangeItemSelection;
        _footerList.SelectionChanged += ChangeItemSelection;
        _bodyList.SelectedIndex = 0;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == SelectedItemProperty || change.Property == BodyItemsSourceProperty ||
            change.Property == FooterItemsSourceProperty)
            UpdateListBox();
    }

    private void UpdateListBox()
    {
        if (_bodyList == null || _footerList == null) return;

        if (SelectedItem == null)
        {
            SelectedItem = BodyItemsSource?.First() ?? FooterItemsSource?.First();
            return;
        }

        if (BodyItemsSource != null && BodyItemsSource.Any(x => x == SelectedItem) &&
            _bodyList.SelectedItem != SelectedItem)
            _bodyList.SelectedItem = SelectedItem;

        if (FooterItemsSource != null && FooterItemsSource.Any(x => x == SelectedItem) &&
            _footerList.SelectedItem != SelectedItem)
            _footerList.SelectedItem = SelectedItem;
    }

    private void ChangeItemSelection(object? sender, SelectionChangedEventArgs e)
    {
        if (_bodyList == null || _footerList == null) return;

        if (sender is not ListBox listBox) throw new Exception("Sender is not a Listbox");
        if (e.AddedItems.Count == 0) return;
        (listBox == _bodyList ? _footerList : _bodyList).SelectedItem = null;
        // Note to self: And it did, this saved my ass
        SelectedItem = listBox.SelectedItem as HamburgerMenuItem ??
                       throw new Exception(
                           "Selected item is not a HamburgerMenuItem, this can't happen but if it does, it's bad");
    }

    public static readonly StyledProperty<HamburgerMenuItem?> SelectedItemProperty =
        AvaloniaProperty.Register<HamburgerMenu, HamburgerMenuItem?>(
            nameof(SelectedItem));

    public HamburgerMenuItem? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DirectProperty<HamburgerMenu, ICommand> ExpandMenuCommandProperty =
        AvaloniaProperty.RegisterDirect<HamburgerMenu, ICommand>(nameof(ExpandMenuCommand), o => o.ExpandMenuCommand);

    public static readonly StyledProperty<bool> IsExpandedProperty = AvaloniaProperty.Register<HamburgerMenu, bool>(
        nameof(IsExpanded));

    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly StyledProperty<double> HamburgerMenuButtonSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(HamburgerMenuButtonSize), defaultValue: 28d);

    public double HamburgerMenuButtonSize
    {
        get => GetValue(HamburgerMenuButtonSizeProperty);
        set => SetValue(HamburgerMenuButtonSizeProperty, value);
    }

    public static readonly StyledProperty<double> HamburgerMenuIconSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(HamburgerMenuIconSize), defaultValue: 16d);

    public double HamburgerMenuIconSize
    {
        get => GetValue(HamburgerMenuIconSizeProperty);
        set => SetValue(HamburgerMenuIconSizeProperty, value);
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
        AvaloniaProperty.Register<HamburgerMenu, double>(nameof(ExpandableWidth), defaultValue: 130d);

    public double ExpandableWidth
    {
        get => GetValue(ExpandableWidthProperty);
        set => SetValue(ExpandableWidthProperty, value);
    }

    public static readonly StyledProperty<double> BodyItemHeightProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(BodyItemHeight), defaultValue: 48d);

    public double BodyItemHeight
    {
        get => GetValue(BodyItemHeightProperty);
        set => SetValue(BodyItemHeightProperty, value);
    }

    public static readonly StyledProperty<double> BodyIconSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(BodyIconSize), defaultValue: 24d);

    public double BodyIconSize
    {
        get => GetValue(BodyIconSizeProperty);
        set => SetValue(BodyIconSizeProperty, value);
    }

    public static readonly StyledProperty<double> BodyFontSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(BodyFontSize));

    public double BodyFontSize
    {
        get => GetValue(BodyFontSizeProperty);
        set => SetValue(BodyFontSizeProperty, value);
    }

    public static readonly StyledProperty<IEnumerable<HamburgerMenuItem>?> BodyItemsSourceProperty =
        AvaloniaProperty.Register<HamburgerMenu, IEnumerable<HamburgerMenuItem>?>(
            nameof(BodyItemsSource));

    public IEnumerable<HamburgerMenuItem>? BodyItemsSource
    {
        get => GetValue(BodyItemsSourceProperty);
        set => SetValue(BodyItemsSourceProperty, value);
    }

    public static readonly StyledProperty<double> FooterItemHeightProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(FooterItemHeight), defaultValue: 30d);

    public double FooterItemHeight
    {
        get => GetValue(FooterItemHeightProperty);
        set => SetValue(FooterItemHeightProperty, value);
    }

    public static readonly StyledProperty<double> FooterIconSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(FooterIconSize), defaultValue: 16d);

    public double FooterIconSize
    {
        get => GetValue(FooterIconSizeProperty);
        set => SetValue(FooterIconSizeProperty, value);
    }

    public static readonly StyledProperty<double> FooterFontSizeProperty =
        AvaloniaProperty.Register<HamburgerMenu, double>(
            nameof(FooterFontSize));

    public double FooterFontSize
    {
        get => GetValue(FooterFontSizeProperty);
        set => SetValue(FooterFontSizeProperty, value);
    }

    public static readonly StyledProperty<IEnumerable<HamburgerMenuItem>?> FooterItemsSourceProperty =
        AvaloniaProperty.Register<HamburgerMenu, IEnumerable<HamburgerMenuItem>?>(
            nameof(FooterItemsSource));

    public IEnumerable<HamburgerMenuItem>? FooterItemsSource
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