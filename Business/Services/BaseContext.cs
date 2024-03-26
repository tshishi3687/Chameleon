namespace Chameleon.Business.Services;

public abstract class BaseContext(Context context)
{
    protected readonly Context Context = context ?? throw new ArgumentNullException(nameof(context));
}