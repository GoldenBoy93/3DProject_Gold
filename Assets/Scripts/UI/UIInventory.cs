using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow; // �� ��ũ��Ʈ�� �޷��ִ� ���ӿ�����Ʈ�� ���ӿ�����Ʈ�� ��Ī
    public Transform slotPanel; // �� ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� Ʈ������ ������Ʈ�� ��Ī
    public Transform dropPosition;      // item ���� �� �ʿ��� ��ġ

    [Header("Selected Item")]           // ������ ������ ������ ���� ǥ�� ���� UI
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private ItemData selectedItem;
    private int selectedItemIndex;

    private int curEquipIndex;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        // Action ȣ�� �� �ʿ��� �Լ� ���
        controller.inventory += Toggle;      // inventory Ű �Է� ��
        CharacterManager.Instance.Player.addItem += AddItem;  // ������ �Ĺ� ��

        // Inventory UI �ʱ�ȭ ������
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }

        ClearSelectedItemWindow();
    }

    // ������ ������ ǥ���� ����â Clear �Լ�
    void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    // Inventory â Open/Close �� ȣ��
    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }


    public void AddItem()
    {
        // 10�� ItemObject �������� Player�� �Ѱ��� ������ ������ ��
        ItemData data = CharacterManager.Instance.Player.itemData;

        // �������� �ߺ� �������� canStack ���� Ȯ��
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                // ������ �߰� �� Player ���ӿ�����Ʈ�� �ִ� itemData�� null�� �ʱ�ȭ (������ �߰� �� �ٽ� �������� ���� �� �ֵ���)
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        // �� ���� ã��
        ItemSlot emptySlot = GetEmptySlot();

        // �� ������ �ִٸ�
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        // �� ���� ���� ���� ��
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }

    // UI ���� ���ΰ�ħ
    public void UpdateUI()
    {
        // �迭�� ��ȸ
        for (int i = 0; i < slots.Length; i++)
        {
            // ������ null�� �ƴϰ� ���Կ� ������ ������ �ִٸ�
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
        }
    }

    // ������ ���� �� �ִ� �������� ���� ã�Ƽ� return
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    // ������ item ������ ����ִ� ���� return
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    // Player ��ũ��Ʈ ���� ����
    // ������ ������ (������ �Ű������� ���� �����Ϳ� �ش��ϴ� ������ ����)
    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }


    // ItemSlot ��ũ��Ʈ ���� ����
    // ������ ������ ����â�� ������Ʈ ���ִ� �Լ�
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        // ��� �����ۿ� ���������� �ִ°� �ƴ϶� �ϴ� Empty�� �ʱ�ȭ
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedItemStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        // PlayerCondition�� Heal �ڷ�ƾ�� ȣ��
                        StartCoroutine(condition.Heal(selectedItem.consumables[i].value));
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value); 
                        break;
                }
            }
            RemoveSelctedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveSelctedItem();
    }

    void RemoveSelctedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            if (slots[selectedItemIndex].equipped)
            {
                UnEquip(selectedItemIndex);
            }

            slots[selectedItemIndex].Clear();

            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }
}