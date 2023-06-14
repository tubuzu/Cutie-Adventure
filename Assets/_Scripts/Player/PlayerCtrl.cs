using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Singleton<PlayerCtrl>
{
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get => playerMovement; }
    [SerializeField] protected PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation => playerAnimation;
    [SerializeField] protected PlayerStatus playerStatus;
    public PlayerStatus PlayerStatus => playerStatus;
    [SerializeField] protected PlayerDamageSender playerDamageSender;
    public PlayerDamageSender PlayerDamageSender => playerDamageSender;
    [SerializeField] protected PlayerPickup playerPickup;
    public PlayerPickup PlayerPickup { get => playerPickup; }
    [SerializeField] protected Inventory inventory;
    public Inventory Inventory { get => inventory; }

    protected override void Awake()
    {
        this.MakeSingleton(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerAnimation();
        this.LoadModel();
        this.LoadPlayerMovement();
        this.LoadPlayerDamageSender();
        this.LoadPlayerStatus();
        this.LoadInventory();
        this.LoadPlayerPickup();
    }
    protected virtual void LoadPlayerPickup()
    {
        if (this.playerPickup != null) return;
        this.playerPickup = transform.Find("PlayerPickup").GetComponent<PlayerPickup>();
        Debug.Log(transform.name + ": LoadPlayerPickup", gameObject);
    }
    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = transform.Find("Inventory").GetComponent<Inventory>();
        Debug.Log(transform.name + ": LoadInventory", gameObject);
    }
    protected virtual void LoadPlayerDamageSender()
    {
        if (this.playerDamageSender != null) return;
        this.playerDamageSender = transform.Find("PlayerDamageSender").GetComponent<PlayerDamageSender>();
        Debug.Log(transform.name + ": LoadPlayerDamageSender", gameObject);
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Transform>();
        // Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = transform.Find("PlayerMovement").GetComponent<PlayerMovement>();
        // Debug.Log(transform.name + ": LoadPlayerMovement", gameObject);
    }
    protected virtual void LoadPlayerAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = transform.Find("Model").GetComponent<PlayerAnimation>();
        Debug.Log(transform.name + ": LoadPlayerAnimation", gameObject);
    }
    protected virtual void LoadPlayerStatus() {
        if (this.playerStatus != null) return;
        this.playerStatus = transform.Find("PlayerStatus").GetComponent<PlayerStatus>();
    }
    // protected virtual void LoadItemCollector() {
    //     if (this.itemCollector != null) return;
    //     this.itemCollector = transform.Find("ItemCollector").GetComponent<ItemCollector>();
    // }
}
