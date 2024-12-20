using Assets.Scripts.Models;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System.Linq;
using System;
using System.Collections.Generic;

public class SityManager : MonoBehaviour
{
    [SerializeField] private TextAsset _sitys;
    private IList<Sity> _sities;
    [SerializeField] private TMP_Dropdown _sityDropdown;
    private string _text => _sitys.text;

    private async void Start()
    {
        _sities = JsonConvert.DeserializeObject<IList<Sity>>(_text);

        _sityDropdown.interactable = false;
        foreach (var item in _sities)
        {
            _sityDropdown.options.Add(new TMP_Dropdown.OptionData($"| {item.name} | {item.subject}"));
        }
        _sityDropdown.RefreshShownValue();
        _sityDropdown.interactable = true;
    }
    public string GetSityNameList()
    {
        return _sities[_sityDropdown.value].name;
    }
    public string GetSitiNameToInput(string input)
    {
        var obj = _sities.FirstOrDefault(x => x.name.Contains(input, StringComparison.InvariantCultureIgnoreCase));
        Debug.Log(obj.name);
        return obj.name;
    }

    public void SetSity(string name)
    {
        string newname = _sities.FirstOrDefault(x => x.name == name).name;
        int index = _sities.IndexOf(_sities.FirstOrDefault(x => x.name.Equals(newname, StringComparison.OrdinalIgnoreCase)));
        _sityDropdown.SetValueWithoutNotify(index);
        _sityDropdown.RefreshShownValue();
    }

}
