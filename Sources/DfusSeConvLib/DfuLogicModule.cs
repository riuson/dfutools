using Autofac;
using DfuSeConvLib.Parts;
using DfuSeConvLib.Serialization;

namespace DfuSeConvLib {
    public class DfuLogicModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<ImageElement>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<TargetPrefix>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuImage>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuImages>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuPrefix>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuSuffix>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<Dfu>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<ImageElementSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<TargetPrefixSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuImageSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuImagesSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuPrefixSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuSuffixSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuSerializer>()
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
