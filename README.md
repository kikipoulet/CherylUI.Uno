# CherylUI.Uno

CherylUI.Uno is a collection of UI controls, helpers and styles built for the [Uno Platform](https://platform.uno/). The library contains custom WinUI controls and resource dictionaries that provide a cohesive look and feel across platforms. A demo application (`CherylUI.Uno.Demo`) is included to showcase the controls.

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

# Controls documentation

## Navigation

### BottomTabControl & BottomTabItem
```xml
<controls:BottomTabControl>
    <controls:BottomTabItem Icon="Home" Label="Home" />
    <controls:BottomTabItem Icon="Setting" Label="Settings" />
</controls:BottomTabControl>
```
<img />

### NavigationView
```xml
<NavigationView>
    <!-- items -->
</NavigationView>
```
<img />

### SegmentedControl
```xml
<ui:Segmented>
    <ui:SegmentedItem Text="One" />
    <ui:SegmentedItem Text="Two" />
</ui:Segmented>
```
<img />

### SimplePageHeader
```xml
<controls:SimplePageHeader Title="My page" />
```
<img />

### SliverPage / SliverPageLong
```xml
<controls:SliverPage Header="My Header">
    <!-- page content -->
</controls:SliverPage>
```
<img />

## Layout & Containers

### BusyArea
```xml
<controls:BusyArea IsBusy="True">
    <Grid><!-- content --></Grid>
</controls:BusyArea>
```
<img />

### GroupBox
```xml
<controls:GroupBox Header="Title">
    <TextBlock Text="Content" />
</controls:GroupBox>
```
<img />

### InteractiveContainer
```xml
<controls:InteractiveContainer>
    <!-- dialogs and sheets appear here -->
</controls:InteractiveContainer>
```
<img />


### Border
- **GlassBorderStyle**
```xml
<Border Style="{StaticResource GlassBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```

- **GlassSoftBorderStyle**
```xml
<Border Style="{StaticResource GlassSoftBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```

- **GlassFrostedBorderStyle**
```xml
<Border Style="{StaticResource GlassFrostedBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```
<img />

## Input controls

### MobileDatePicker
```xml
<pickers:MobileDatePicker />
```
<img />

### MobilePicker
```xml
<pickers:MobilePicker ItemsSource="{Binding Options}" />
```
<img />

### MobileTextBox
```xml
<pickers:MobileTextBox PlaceholderText="Type here" />
```
<img />

### MobileTimePicker
```xml
<pickers:MobileTimePicker />
```
<img />

### CalendarView
```xml
<CalendarView />
```
<img />


### CheckBox
```xml
<CheckBox Content="Accept" />
```
<img />

### NumberBox
```xml
<NumberBox PlaceholderText="0" />
```
<img />

### RadioButton
```xml
<RadioButton Content="Option" />
```
<img />

### Slider
```xml
<Slider Minimum="0" Maximum="100" />
```
<img />

### ToggleSwitch
```xml
<ToggleSwitch />
```
<img />


### TextBlock
```xml
<TextBlock Text="Styled text" />
```
<img />

### Buttons

- **DefaultButtonStyle**
```xml
<Button Content="Default" Style="{StaticResource DefaultButtonStyle}" />
```

- **SecondaryButtonStyle**
```xml
<Button Content="Secondary" Style="{StaticResource SecondaryButtonStyle}" />
```

- **MiniButtonStyle**
```xml
<Button Content="Mini" Style="{StaticResource MiniButtonStyle}" />
```

- **SmallButtonStyle**
```xml
<Button Content="Small" Style="{StaticResource SmallButtonStyle}" />
```

- **BigButtonStyle**
```xml
<Button Content="Big" Style="{StaticResource BigButtonStyle}" />
```

- **BigNormalButtonStyle**
```xml
<Button Content="Big normal" Style="{StaticResource BigNormalButtonStyle}" />
```

- **LargeButtonStyle**
```xml
<Button Content="Large" Style="{StaticResource LargeButtonStyle}" />
```

- **TextButtonStyle**
```xml
<Button Content="Text" Style="{StaticResource TextButtonStyle}" />
```

- **VoidButtonStyle**
```xml
<Button Content="Void" Style="{StaticResource VoidButtonStyle}" />
```


## Menus

### MenuFlyoutPresenter
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
