using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class CraftManager : Singleton<CraftManager>
{
   [SerializeField] private TextAsset Form;
   private List<CraftData> FormData;
   private void Awake()
    {
        FormData = JsonConvert.DeserializeObject<RootForm>(Form.text).CraftForm;
    }

   public string Craft(string[] Data)
   {
       bool NotThis=false;
       foreach (var VARIABLE in FormData)
       {
           var Element = VARIABLE.ElementName;
           if (Element.Length == Data.Length)
           {
               foreach (var item in Data)
               {
                   if (!Element.Contains(item))
                   {
                       NotThis =true;
                   }
               }
               if (!NotThis)
               {
                   return VARIABLE.CompoundName;
               }
           }
       }

       return null;
   }
   public string Craft(List<string> Data)
   {
       bool NotThis=false;
       foreach (var VARIABLE in FormData)
       {
           var Element = VARIABLE.ElementName;
           if (Element.Length == Data.Count)
           {
               foreach (var item in Data)
               {
                   if (!Element.Contains(item))
                   {
                       NotThis =true;
                   }
               }
               if (!NotThis)
               {
                   return VARIABLE.CompoundName;
               }
           }
       }

       return null;
   }
   private void Start()
   {
   }
}
[SerializeField] public class CraftData
{
    public string[] ElementName;
    public string CompoundName;
}
[SerializeField]public class RootForm
{
    public List<CraftData> CraftForm;

}