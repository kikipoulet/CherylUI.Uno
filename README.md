# CherylUI.Uno

CherylUI.Uno is a collection of UI controls, helpers and styles built for the [Uno Platform](https://platform.uno/). The library contains custom WinUI controls and resource dictionaries that provide a cohesive look and feel across platforms. A demo application (`CherylUI.Uno.Demo`) is included to showcase the controls.

## Repository layout

```
Cheryl.Uno/            Library project containing controls, helpers and styles
 ├── Controls/         Custom controls used by applications
 ├── Helpers/          Utility classes such as animation helpers and easings
 ├── Styles/           XAML resource dictionaries with control styles and colors
CherylUI.Uno.Demo/     Sample application demonstrating the library
```

The `Styles/Index.xaml` file merges all provided style dictionaries. Apps typically merge this single dictionary into `Application.Resources`.

## Using the library

1. Reference the `Cheryl.Uno` project or NuGet package in your Uno Platform solution.
2. Merge the style dictionary in `App.xaml`:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="ms-appx:///Cheryl.Uno/Styles/Index.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

3. Use the controls by declaring the namespace and dropping them into your XAML:

```xml
<Page
    xmlns:controls="using:Cheryl.Uno.Controls">
    <controls:BottomTabControl>
        <!-- ... -->
    </controls:BottomTabControl>
</Page>
```

## Custom controls

The library exposes the following controls. Each control has a default style defined in `Styles/`.

| Control | Description |
|---------|-------------|
| **CherylBackgroundWinUI3** | Animated noise background used by `InteractiveContainer` |
| **BottomTabControl** & **BottomTabItem** | Tab navigation control placed at the bottom of the screen |
| **BusyArea** | Container that shows a `ProgressRing` overlay when `IsBusy` is true |
| **GroupBox** | Simple content container with a header |
| **InteractiveContainer** | Hosts dialogs, bottom sheets and toast content |
| **SimplePageHeader** | Basic page header with a centered title |
| **SliverPage** | Page layout with a collapsing header when scrolled |
| **SliverPageLong** | Variant of `SliverPage` with a larger header |
| **MobileDatePicker** / **MobileDatePickerPanel** | Bottom-sheet style date picker |
| **MobilePicker** / **MobilePickerPopup** | Generic list picker presented in a bottom sheet |
| **MobileTextBox** / **MobileTextBoxDialog** | Text input that opens a bottom dialog for editing |
| **MobileTimePicker** / **MobileTimePickerPanel** | Bottom-sheet style time picker |

## Controls and styles by category

Below each example you'll find an `<img />` placeholder where a screenshot should be inserted.

### Navigation

#### BottomTabControl & BottomTabItem
```xml
<controls:BottomTabControl>
    <controls:BottomTabItem Icon="Home" Label="Home" />
    <controls:BottomTabItem Icon="Setting" Label="Settings" />
</controls:BottomTabControl>
```
<img />

#### NavigationView
```xml
<NavigationView>
    <!-- items -->
</NavigationView>
```
<img />

#### SegmentedControl
```xml
<ui:Segmented>
    <ui:SegmentedItem Text="One" />
    <ui:SegmentedItem Text="Two" />
</ui:Segmented>
```
<img />

#### SimplePageHeader
```xml
<controls:SimplePageHeader Title="My page" />
```
<img />

#### SliverPage / SliverPageLong
```xml
<controls:SliverPage Header="My Header">
    <!-- page content -->
</controls:SliverPage>
```
<img />

### Layout & Containers

#### BusyArea
```xml
<controls:BusyArea IsBusy="True">
    <Grid><!-- content --></Grid>
</controls:BusyArea>
```
<img />

#### GroupBox
```xml
<controls:GroupBox Header="Title">
    <TextBlock Text="Content" />
</controls:GroupBox>
```
<img />

#### InteractiveContainer
```xml
<controls:InteractiveContainer>
    <!-- dialogs and sheets appear here -->
</controls:InteractiveContainer>
```
<img />

#### SplitView
```xml
<SplitView>
    <!-- content -->
</SplitView>
```
<img />

#### CherylBackgroundWinUI3
```xml
<controls:CherylBackgroundWinUI3 />
```
<img />

#### Glass border styles
- **GlassBorderStyle**
```xml
<Border Style="{StaticResource GlassBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```
<img />
- **GlassSoftBorderStyle**
```xml
<Border Style="{StaticResource GlassSoftBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```
<img />
- **GlassFrostedBorderStyle**
```xml
<Border Style="{StaticResource GlassFrostedBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```
<img />

### Input controls

#### MobileDatePicker
```xml
<pickers:MobileDatePicker />
```
<img />

#### MobilePicker
```xml
<pickers:MobilePicker ItemsSource="{Binding Options}" />
```
<img />

#### MobileTextBox
```xml
<pickers:MobileTextBox PlaceholderText="Type here" />
```
<img />

#### MobileTimePicker
```xml
<pickers:MobileTimePicker />
```
<img />

#### CalendarView
```xml
<CalendarView />
```
<img />

#### ComboBox
```xml
<ComboBox ItemsSource="{Binding Items}" />
```
<img />

#### CheckBox
```xml
<CheckBox Content="Accept" />
```
<img />

#### NumberBox
```xml
<NumberBox PlaceholderText="0" />
```
<img />

#### RadioButton
```xml
<RadioButton Content="Option" />
```
<img />

#### Slider
```xml
<Slider Minimum="0" Maximum="100" />
```
<img />

#### ToggleSwitch
```xml
<ToggleSwitch />
```
<img />

#### ListViewItem
```xml
<ListView>
    <ListViewItem Content="Item" />
</ListView>
```
<img />

#### TextBlock
```xml
<TextBlock Text="Styled text" />
```
<img />

### Buttons

- **DefaultButtonStyle**
```xml
<Button Content="Default" Style="{StaticResource DefaultButtonStyle}" />
```
<img />
- **SecondaryButtonStyle**
```xml
<Button Content="Secondary" Style="{StaticResource SecondaryButtonStyle}" />
```
<img />
- **MiniButtonStyle**
```xml
<Button Content="Mini" Style="{StaticResource MiniButtonStyle}" />
```
<img />
- **SmallButtonStyle**
```xml
<Button Content="Small" Style="{StaticResource SmallButtonStyle}" />
```
<img />
- **BigButtonStyle**
```xml
<Button Content="Big" Style="{StaticResource BigButtonStyle}" />
```
<img />
- **BigNormalButtonStyle**
```xml
<Button Content="Big normal" Style="{StaticResource BigNormalButtonStyle}" />
```
<img />
- **LargeButtonStyle**
```xml
<Button Content="Large" Style="{StaticResource LargeButtonStyle}" />
```
<img />
- **TextButtonStyle**
```xml
<Button Content="Text" Style="{StaticResource TextButtonStyle}" />
```
<img />
- **VoidButtonStyle**
```xml
<Button Content="Void" Style="{StaticResource VoidButtonStyle}" />
```
<img />

### Menus

#### MenuFlyoutPresenter
```xml
<Button Content="Open">
    <Button.Flyout>
        <MenuFlyout>
            <MenuFlyoutItem Text="Action" />
        </MenuFlyout>
    </Button.Flyout>
</Button>
```
<img />

## Running the demo

The `CherylUI.Uno.Demo` project demonstrates the controls. Open `CherylUI.Uno.Demo.sln` with Visual Studio and run the platform-specific head (e.g. Windows) to see the styles and controls in action.

---
This README provides a high-level overview of the repository. Explore the source files in `Cheryl.Uno` and the demo project for additional details and examples.
