Signature Pad makes capturing, saving, exporting, and displaying
signatures extremely simple.

## Examples

### Displaying a signature pad

On iOS:

```csharp
using SignaturePad;
...

public override void ViewDidLoad ()
{
	...
	var signature = new SignaturePadView (View.Frame) {
		LineWidth = 3f
	};
	View.AddSubview (signature);
}
```

On Android:

```csharp
using SignaturePad;
...

protected override void OnCreate (Bundle bundle)
{
	base.OnCreate (bundle);

	var signature = new SignaturePadView (this) {
		LineWidth = 3f
	};
	AddContentView (signature,
		new ViewGroup.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent));
}
```

### Getting the signature as an image

```csharp
// on iOS:
UIImage image = signature.GetImage ();

// on Android:
Bitmap image = signature.GetImage ();
```

### Getting the signature as a point array

```csharp
// Discontinuous lines are separated by PointF.Empty
PointF[] points = signature.Points;
```

### Loading a signature from a point array

```csharp
signature.LoadPoints (points);
```
