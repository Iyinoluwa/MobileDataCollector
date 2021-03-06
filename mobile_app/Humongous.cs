using Android.App;
using Android.Content.Res;
using MobileCollector.model;
using MobileCollector.store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileCollector
{
    public class AppInstance
    {
        static AppInstance _instance;
        public static AppInstance Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppInstance()
                    {
                        AppVersion = "1.0"
                    }
                        ;
                }
                return _instance;
            }
        }

        AssetManager _assetManager { get; set; }
        Activity _mainContext;
        public LocalEntityStore LocalEntityStoreInstance { get; private set; }

        public Dictionary<int, List<FieldValuePair>> TemporalViewData = null;

        public Dictionary<string, string> ApiAssets = null;
        public void InitialiseAppResources(AssetManager assetManager, Activity context)
        {
            //ContextManager = null;
            ModuleContext = null;
            _assetManager = assetManager;
            _mainContext = context;
            TemporalViewData = new Dictionary<int, List<FieldValuePair>>();
            ApiAssets = new Dictionary<string, string>();
            //we read the api key file
            var inputStream = assetManager.Open(Constants.API_KEYFILE);
            var jsonObject = System.Json.JsonValue.Load(inputStream);

            foreach (var assetName in Constants.ASSET_LIST)
            {
                ApiAssets[assetName] =
                    jsonObject.decryptAndGetApiSetting(assetName);
            }

            //we need to have this class initialised
            LocalEntityStoreInstance = new LocalEntityStore();
            LocalEntityStoreInstance.buildTables(false);
            
            CloudDbInstance = new CloudDb(_assetManager);

            var configuration = new LocalDB3().DB.Table<DeviceConfiguration>().FirstOrDefault();
            Configuration = configuration;
        }

        public projects.ContextLocalEntityStore ModuleContext { get; set; }

        internal void SetProjectContext(projects.BaseContextManager ctxt)
        {
            ModuleContext = new projects.ContextLocalEntityStore(ctxt);
        }

        List<FieldItem> readFields(string fieldsAssetName, AssetManager assetManager, Activity context)
        {
            //we load the fields
            var fieldStream = assetManager.Open(fieldsAssetName);

            var asString = fieldStream.toText();

            var fields = Newtonsoft.Json.JsonConvert.
                DeserializeObject<List<FieldItem>>(asString);

            var viewPages = fields.Select(t => t.pageName).Distinct().ToList();

            var dictionary = new Dictionary<string, int>();
            foreach (var page in viewPages)
            {
                var id = context.Resources.GetIdentifier(page, "layout", context.PackageName);
                if (id == 0) throw new ArgumentOutOfRangeException("Could not determine Id for layout " + page);
                dictionary[page] = id;
            }

            foreach (var field in fields)
            {
                field.PageId = dictionary[field.pageName];
            }

            return fields;
        }

        internal void LogActionItem(string v)
        {
            //todo: implement LogActionItem in AppInstance
            //throw new NotImplementedException();
        }

        public UserSession CurrentUser { get; internal set; }

        internal void SetTempDataForView(int viewId, List<FieldValuePair> valueFields)
        {
            throw new NotImplementedException();
        }

        public CloudDb CloudDbInstance
        {
            get;private set;
        }
        public DeviceConfiguration Configuration { get; internal set; }
        public string AppVersion { get; internal set; }
    }
}