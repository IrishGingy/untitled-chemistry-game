using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modal : MonoBehaviour
{
    public Dictionary<ModalType, List<string>> modalLists = new Dictionary<ModalType, List<string>>();
}

public enum ModalType
{
    Location,
    WeightRange,
    Name,
}
