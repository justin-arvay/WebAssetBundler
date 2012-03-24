
namespace ResourceCompiler.Web.Mvc
{
    using System;

    public class WebAssetGroupCollectionBuilder
    {

        private WebAssetType type;

        private WebAssetGroupCollection resourceGroups;

        public WebAssetGroupCollectionBuilder(WebAssetType type, WebAssetGroupCollection resourceGroups)
        {
            this.type = type;
            this.resourceGroups = resourceGroups;
        }


        public WebAssetGroupCollectionBuilder AddGroup(string name, Action<WebAssetGroupBuilder> configureAction)
        {
            //ensure that we cannot add the same group twice
            if (resourceGroups.FindGroupByName(name) != null) 
            {
                throw new ArgumentException(TextResource.Exceptions.GroupWithSpecifiedNameAlreadyExists);
            }

            var group = new WebAssetGroup(name, false) ;

            //add to collection
            resourceGroups.Add(group);

            //call action
            configureAction(new WebAssetGroupBuilder(group));
            return this;
        }

        /// <summary>
        /// Adds a file to the collection.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public WebAssetGroupCollectionBuilder Add(string source)
        {
            var group = new WebAssetGroup("Single", false) ;

            group.Assets.Add(new WebAsset(source));

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
        public WebAssetGroupCollectionBuilder AddSharedGroup(string name, Action<WebAssetGroupBuilder> configureAction)
        {
            throw new NotImplementedException();
            return this;
        }
    }
}
