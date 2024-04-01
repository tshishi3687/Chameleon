namespace Chameleon.Business.Services;

public abstract class IContext(Context context)
{
    protected readonly Context Context = context ?? throw new ArgumentNullException(nameof(context));
}