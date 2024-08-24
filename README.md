> # DependencyInjection_Service
> 
> - builder.Services.AddSingleton<ISingletonService, SingletonService>();
> - builder.Services.AddScoped<IScopedService, ScopedService>();
> - builder.Services.AddTransient<ITransientService, TransientService>();
> 
> ### Usage
> <pre>
> public HomeController( 
>   ISingletonService singleton1, ISingletonService singleton2,          // always same 
>   IScopedService    scoped1,    IScopedService scoped2,                // scoped1, scoped1 value is different if HTTP is different 
>   ITransientService transient1, ITransientService transient2           // transient1, transient2 value is different 
> )
> </pre>
