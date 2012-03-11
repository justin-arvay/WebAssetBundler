
namespace ResourceCompiler.Resource
{
    using System;

    public class ResourceGroupCollectionBuilder
    {

        private ResourceType type;

        private ResourceGroupCollection resourceGroups;

        public ResourceGroupCollectionBuilder(ResourceType type, ResourceGroupCollection resourceGroups)
        {
            this.type = type;
            this.resourceGroups = resourceGroups;
        }


        public ResourceGroupCollectionBuilder AddGroup(string name, Action<ResourceGroupBuilder> configureAction)
        {
            //ensure that we cannot add the same group twice
            if (resourceGroups.FindGroupByName(name) != null) 
            {
                throw new ArgumentException(TextResource.Exceptions.GroupWithSpecifiedNameAlreadyExists);
            }

            var group = new ResourceGroup(name, false) ;

            //add to collection
            resourceGroups.Add(group);

            //call action
            configureAction(new ResourceGroupBuilder(group));
            return this;
        }

        /// <summary>
        /// Adds a file to the collection.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ResourceGroupCollectionBuilder Add(string source)
        {
            var group = new ResourceGroup("Single", false) ;

            group.Resources.Add(new Files.Resource(source));

            //add to collection
            resourceGroups.Add(group);

            return this;
        }

        /// <summary>
        /// Adds a new group and allows you to configure that group, or add an existing group as a shared group and configure it differently for this use.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public ResourceGroupCollectionBuilder AddSharedGroup(string name, Action<ResourceGroupBuilder> configureAction)
        {
            throw new NotImplementedException();
            return this;
        }
    }
}
