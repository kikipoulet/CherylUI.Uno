<div id="header" align="center">
 <kbd>

<img width="250" height="250" alt="This logo is creepy af"  src="https://github.com/user-attachments/assets/559f0cad-18a0-44d9-b0d1-b720d5b5bd01" />


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

![busyarea](https://github.com/user-attachments/assets/12307641-1f46-402b-95d6-6b8af5ccf594)

```xml
<controls:BusyArea IsBusy="True">
    <!-- content -->
</controls:BusyArea>
```
<br/>

### GroupBox

<img width="334" height="211" alt="{4C47DAAE-7D85-4931-A5A1-3DC41AB46BE9}" src="https://github.com/user-attachments/assets/2305c5a7-6900-48a7-8fee-6da865a02f73" />


```xml
<controls:GroupBox Header="Title">
    <!-- content -->
</controls:GroupBox>
```
<br/>


### Border
- **GlassBorderStyle**

<img width="336" height="122" alt="{3224DC18-4478-4FF4-B148-262CD80C4358}" src="https://github.com/user-attachments/assets/3267a1c1-1189-4e8d-ab6e-6508a297477b" />

Basically the border you see everywhere in that library.

```xml
<Border Style="{StaticResource GlassBorderStyle}">
</Border>
```

<br/>



- **GlassSoftBorderStyle**

<img width="327" height="258" alt="{DE6148F5-E411-45BF-9156-64E103655F7C}" src="https://github.com/user-attachments/assets/e2f22b0e-c241-40b9-8d44-4f8aee19dbeb" />

Like GlassBorderStyle but softer, perfect to be used on a GlassBorder because of a dialog or a bottomsheet for example.
  
```xml
<Border Style="{StaticResource GlassSoftBorderStyle}">
    <TextBlock Text="Inside" />
</Border>
```
<br/>


- **GlassFrostedBorderStyle**

<img width="335" height="394" alt="{04CD0B96-E407-4721-B396-ABE685EE4B59}" src="https://github.com/user-attachments/assets/f3dbd9be-2851-4906-a41e-75c44f61d716" />


Normal GlassBorder but frosted behind. To use if you need to overlay something.

```xml
<Border Style="{StaticResource GlassFrostedBorderStyle}">
</Border>
```
<br/>


## Input controls

### MobileDatePicker

![datepicker](https://github.com/user-attachments/assets/ccdaf821-83d7-4aa9-b454-5e964256ed19)


```xml
<pickers:MobileDatePicker  Date="{x:Bind SelectedDate}" />
```
<br/>

### MobilePicker

![pikcer](https://github.com/user-attachments/assets/97732f4a-14b3-4ef2-b5a4-73ec42b78bdf)


```xml
<pickers:MobilePicker ItemsSource="{Binding Options}"  SelectedItem="{Binding SelectedOption}"/>
```
<br/>

### MobileTextBox

![mobileTB](https://github.com/user-attachments/assets/45e4d410-1520-4bef-afd8-7323e2517a38)


```xml
<pickers:MobileTextBox  PlaceholderText="Enter name" PopupTitle="What is your First Name ?" Text="Billy" />
```
<br/>

### MobileTimePicker

![mobileTimepicker](https://github.com/user-attachments/assets/30599e5e-519f-43a8-a6c6-f79d62b1d55b)


```xml
<pickers:MobileTimePicker Time="{x:Bind SelectedTime}" />
```
<br/>


### CheckBox
```xml
<CheckBox Content="Accept" />
```
<br/>

### NumberBox
```xml
<NumberBox PlaceholderText="0" />
```
<br/>

### RadioButton
```xml
<RadioButton Content="Option" />
```
<br/>

### Slider
```xml
<Slider Minimum="0" Maximum="100" />
```
<br/>

### ToggleSwitch
```xml
<ToggleSwitch />
```
<br/>


### TextBlock
```xml
<TextBlock Text="Styled text" />
```
<br/>

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
<br/>
