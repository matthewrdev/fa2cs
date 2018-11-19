# About FontAwesome.IconCodes

Important!
FontAwesome.IconCodes does not add the FontAwesome font files to your application.
You MUST follow the platform specific instructions for including your font files.

C# Usage:

var fileIcon = FontAwesome.FontAwesomeIcons.Alicorn;

XAML Usage:

 * Add a namespace reference to FontAwesome.IconCodes: xmlns:fontAwesome="clr-namespace:FontAwesome;assembly=FontAwesome.IconCodes"
 * Use x:Static to reference an icon code: Text="{x:Static fontAwesome:FontAwesomeIcons.Alicorn}"