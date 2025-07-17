<div id="header" align="center">
 <kbd>


<img width="250" height="250" alt="This logo is creepy af" src="https://github.com/user-attachments/assets/828f0e33-025a-4251-b8e5-01cfc460274d" />


  </kbd>
<br/>

</div>
<br/>


# ✨ CherylUI.Uno

CherylUI.Uno is a collection of UI controls, helpers and styles built for the [Uno Platform](https://platform.uno/). The library contains custom WinUI controls and resource dictionaries that provide a cohesive look and feel across platforms. A demo application (`CherylUI.Uno.Demo`) is included to showcase the controls.

<br/>

# 📱 UI Demo


https://github.com/user-attachments/assets/04cf44dd-e075-41f7-a664-e78650e0970b



https://github.com/user-attachments/assets/067a6a66-cf39-46c7-8d78-0a6750e17b06


https://github.com/user-attachments/assets/434dbcab-9d31-4aeb-a758-baa73dfca636


<br/>

# Temporary Documentation

## Using the library

1. Reference the `CherylUI.Uno` project or NuGet package in your Uno Platform solution.
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


## Navigation

### BottomTabControl & BottomTabItem


<img width="337" height="137" alt="{BA2D3CBF-BC92-41C1-B3B8-238E09506880}" src="https://github.com/user-attachments/assets/c369cf95-9199-47cb-901c-e12876b0d17f" />


```xml
<controls:BottomTabControl>
    <controls:BottomTabItem IconGlyph="&#xE80F;" Label="Home" />
    <controls:BottomTabItem IconGlyph="&#xE80F;" Label="Settings" />
</controls:BottomTabControl>
```

<br/>

### NavigationView

<img width="332" height="554" alt="{6F64CF61-F0B5-4438-8C3F-F51BD97E7A94}" src="https://github.com/user-attachments/assets/a350f798-3e6b-4480-83e7-5e85cc99d095" />


```xml
    <!-- Supposed to work like a standard WinUI NavigationView -->
```

<br/>

### SegmentedControl

<img width="170" height="69" alt="{A7C8D04D-D936-4D4F-A0BA-E4B5A36E1088}" src="https://github.com/user-attachments/assets/8e71bf84-e2d2-4bd3-8f16-c77da52a4b88" />


```xml
<ui:Segmented>
    <ui:SegmentedItem Content="60hz" />
    <ui:SegmentedItem Content="120hz" />
</ui:Segmented>
```
<br/>

### SimplePageHeader

<img width="335" height="119" alt="{6FC61356-4B87-494F-B3E7-7A6D1C26CFD3}" src="https://github.com/user-attachments/assets/4964da15-10b4-466f-80b3-aada64979559" />


```xml
<controls:SimplePageHeader Header="Bob" />
```
<br/>

### SliverPage 

![sliverpage](https://github.com/user-attachments/assets/682bb6af-344b-49c4-bbce-3e36eff85ea4)


```xml
<controls:SliverPage Header="Buttons">
    <!-- page content -->
</controls:SliverPage>
```
<br/>

### SliverPageLong 

![sliverpagelong](https://github.com/user-attachments/assets/077229ed-dad4-4277-9715-2cd4edd7c7cd)


```xml
<controls:SliverPageLong Header="Settings">
    <!-- page content -->
</controls:SliverPageLong>
```
<br/>

## Layout & Containers

### BusyArea
```xml
<controls:BusyArea IsBusy="True">
    <Grid><!-- content --></Grid>
</controls:BusyArea>
```
<br/>

### GroupBox
```xml
<controls:GroupBox Header="Title">
    <TextBlock Text="Content" />
</controls:GroupBox>
```
<br/>

### InteractiveContainer
```xml
<controls:InteractiveContainer>
    <!-- dialogs and sheets appear here -->
</controls:InteractiveContainer>
```
<br/>

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
