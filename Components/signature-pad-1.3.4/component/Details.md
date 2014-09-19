Signature Pad makes capturing, saving, exporting, and displaying
signatures extremely simple.

Adding a `SignaturePadView` to your iOS app:

```csharp
using SignaturePad;
...

public override void ViewDidLoad ()
{
	...
	var signature = new SignaturePadView (View.Frame);
	View.AddSubview (signature);
}
```

Adding a `SignaturePadView` to your Android app:

```csharp
using SignaturePad;
...

protected override void OnCreate (Bundle bundle)
{
	base.OnCreate (bundle);

	var signature = new SignaturePadView (this);
	AddContentView (signature,
		new ViewGroup.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent));
}
```

To capture the user's signature as an image:

```csharp
var image = signature.GetImage ();
```

Customization
-------------

You can change some of the positioning, colors, fonts and the background image of the SignaturePad
using a few interfaces that the control provides and standard techniques provided by the platform.

### SignaturePad customization interface

The class for both iOS and Android expose some of its internal elements to allow text, font, color and positioning manipulation from your code:

`StrokeColor` Sets the color of the signature input.

`StrokeWidth` Sets the width of the signature input.

`BackgroundColor` Sets the color for the whole signature pad.

`SignatureLineColor` The color of the horizontal line.

`SignaturePrompt` The text label containing the symbol or text that goes under the horizontal line (Default "X").

`Caption` The text label that goes under the horizontal line.

`SignatureLine` The view that is used to render the horizontal line.

`ClearLabel` The view that when clicked clears the pad.

`BackgroundImageView` An optional image rendered below the input strokes that can be used as a texture, logo or watermark.

### iOS customization tips

Check the sample for ideas on how to manipulate the layout to get the desired effects and color.

You can alter the subviews Frames or if you are targeting above iOS 6, use Auto-layout constraints to reposition elments within the pad. For coloring, reasign properties such as BackgroundColor (including UIColor.Clear for a transparent view).

BackgroundImageView cannot be set, but its Image member can, so you can assign a bitmap pulled from a resource or wherever you may get its data. Change the Alpha to make it semi-transparent to get a watermark effect or create a texture using a bitmap with the same dimensions as the pad.

If you don't want the SignaturePrompt, the Caption or the SignatureLine to appear inside your pad, just assign
their Hidden property to true.

SignaturePad.Layer can be manipulated to generate or remove the shadow from the control or alter its thickness or roundness.

### Android customization tips

Check the sample for ideas on how to manipulate the layout to get the desired effects and color.

Under Android, the control inherits from RelativeLayout, which provides a good amount of flexibility for repositioning of the child views within the pad. Assign for the children the LayoutParameters property with new RelativeLayout.LayoutParams to move the elements around or resize them using relative positioning policies. All of the elements within the pad have Ids already set so you can establish relative positions between them easily.

BackgroundImageView cannot be set, but you can assign it new data using the SetImage* methods and then aler it with SetAlpha to make it semi-transparent and get a watermark effect or create a texture effect (remember to resize it to the full extent of its parent, the SignaturePad).

If you don't want the SignaturePrompt, the Caption or the SignatureLine to appear inside your pad, just assign
their Visibility property to ViewStates.Invisible.

*Screenshot created with [PlaceIt](http://placeit.breezi.com).*
