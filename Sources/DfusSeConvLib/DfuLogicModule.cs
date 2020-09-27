using Autofac;
using DfuSeConvLib.Deserialization;
using DfuSeConvLib.Exceptions;
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

            builder.RegisterType<DeserializerException>()
                .AsSelf();
            builder.RegisterType<ImageElementDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<TargetPrefixDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuImageDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuImagesDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuPrefixDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuSuffixDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterType<DfuDeserializer>()
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
