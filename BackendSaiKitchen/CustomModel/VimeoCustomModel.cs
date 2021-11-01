using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class VimeoCustomModel
    {
		public class VimeoVideoChannel
		{
			public String VideoID;
			public String VideoName;

			public VimeoVideoChannel(String ID, String Name)
			{
				VideoID = ID;
				VideoName = Name;
			}
		}

		public class VideoDownloadURL
		{
			public String VideoURL;
			public String VideoName;

			public VideoDownloadURL(String URL, String Name)
			{
				VideoURL = URL;
				VideoName = Name;
			}
		}
	}
}
