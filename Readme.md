# About FontAwesome To C#

FontAwesome To C# (fa2cs) creates a C# file, [FontAwesomeIcons.cs](FontAwesomeIcons.cs), that contains string constants for all FontAwesome icon codes.

Use FontAwesome To C# to replace confusing and arcane unicode strings with a clean and descriptive property assignment for more readable and maintainable code.

This:

```
submitButton.Text = "/uf00c";
```

Becomes this:

```
submitButton.Text = FontAwesome.FontAwesomeIcons.Check;
```

# Using FontAwesome To C#

It's super easy to use FontAwesome To C#... Simply download [FontAwesomeIcons.cs](FontAwesomeIcons.cs) and place it into your project:

![Placing FontAwesomeIcons.cs inside a C# project](img/usage.png)

**Ensure that you have added the FontAwesome font files into your projects!**

You can use an icon in C# like:

```
var fileIcon = FontAwesome.FontAwesomeIcons.Alicorn;
```

You can use an icon in XAML by:

 * Adding a namespace reference to `FontAwesome`: `xmlns:fontAwesome="clr-namespace:FontAwesome"`;
 * Referencing a icon using `x:Static`: `<Label Text="{x:Static fontAwesome:FontAwesomeIcons.Alicorn}" FontFamily=""/>`

Voila! All done!

# Hey! Wheres my NuGet package?

`FontAwesomeIcons.cs` is one file that will rarely change... Is an entire package dependency really necessary for one file? 🤔

# Credits

 * [Font Awesome](https://fontawesome.com/): The amazing Font Awesome icon set.
 * [HtmlAgility Pack](https://html-agility-pack.net/): Used for crawling www.fontawesome.com 🙈
