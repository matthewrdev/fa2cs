# About

**Last Generated for FontAwesome v$latest_version$ on $exported_date_time$ ($exported_timezone$)**

Use `FontAwesomeIcons.cs` to replace confusing and arcane unicode strings with a clean and descriptive property.

This:

```
// Huh? What icon is this? What font is it from? 😭
submitButton.Text = "/uf00c";
```

Becomes this:

```
// Obviously a check icon from FontAwesome! 😊👍
submitButton.Text = FontAwesome.FontAwesomeIcons.Check;
```

The end result is cleaner, more readable and more maintainable code.

**[Get FontAwesomeIcons.cs here](https://raw.githubusercontent.com/matthewrdev/fa2cs/master/FontAwesomeIcons.cs)**

**[Download the FontAwesome font assets here](https://github.com/FortAwesome/Font-Awesome/tree/master/webfonts)**

# Using FontAwesome To C#

It's super easy to use FontAwesome To C#!

Simply download [FontAwesomeIcons.cs](FontAwesomeIcons.cs) and place it into your project.

**[Ensure that you have added the FontAwesome font files into your projects.](https://github.com/FortAwesome/Font-Awesome/tree/master/webfonts)**

You can use an icon in C# like:

```
var checkIcon = FontAwesome.FontAwesomeIcons.Check;
```

You can use an icon in XAML by:

 * Adding a namespace reference to `FontAwesome`: `xmlns:fontAwesome="clr-namespace:FontAwesome"`
 * Referencing a icon using `x:Static`: `<Label Text="{x:Static fontAwesome:FontAwesomeIcons.Check}"/>`

Voila! All done!

# Using Material Design Icons?

If you're using the Material Design icon set, [check out md2cs](https://github.com/matthewrdev/md2cs), a static class file containing string constants for all Material Design icon codes.
