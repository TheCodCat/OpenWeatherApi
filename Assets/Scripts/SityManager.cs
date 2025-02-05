using Assets.Scripts.Models;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System.Linq;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class SityManager : MonoBehaviour
{
    private IList<Sity> _sities;
    [SerializeField] private TextAsset _asset;
    private string _json;
    [SerializeField] private TMP_Dropdown _sityDropdown;

    public async UniTask BootInit()
    {
        try
        {
            _sities = JsonConvert.DeserializeObject<IList<Sity>>(_asset.text);
            Debug.Log(_sities.Equals(null) ? "Листа нету" : _sities.Count);

            _sityDropdown.interactable = false;
            foreach (var item in _sities)
            {
                _sityDropdown.options.Add(new TMP_Dropdown.OptionData($"| {item.name} | {item.subject}"));
            }
            _sityDropdown.RefreshShownValue();
            _sityDropdown.interactable = true;

            await UniTask.Yield();
        }
        catch (Exception ex)
        {
            Debug.Log("Произошла ошибка при загрузки бд");
        }
    }
    public string GetSityNameList()
    {
        return _sities[_sityDropdown.value].name;
    }
    public string GetSitiNameToInput(string input)
    {
        try
        {
            var obj = _sities.FirstOrDefault(x => x.name.Contains(input, StringComparison.InvariantCultureIgnoreCase));
            SetSity(name);
            Debug.Log(obj.name);
            return obj.name;
        }
        catch (NullReferenceException ex)
        {
            return input;
        }
    }

    public void SetSity(string name)
    {
        try
        {
            string newname = _sities.FirstOrDefault(x => x.name == name).name ?? "";
            int index = _sities.IndexOf(_sities.FirstOrDefault(x => x.name.Equals(newname, StringComparison.OrdinalIgnoreCase)));
            _sityDropdown.SetValueWithoutNotify(index);
            _sityDropdown.RefreshShownValue();
        }
        catch(NullReferenceException ex)
        {
            Debug.Log($"Такого города не найдено {name}");
        }
    }

}
