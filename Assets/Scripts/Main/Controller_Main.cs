﻿using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller_Main : MonoBehaviour {
    private static Controller_Main instance;
    public event EventHandler ClearUI;
    public event EventHandler UnitSelected;
    [HideInInspector]
    public Transform character;
    public SkillMenu skillMenu;
    public ItemMenu itemMenu;
    public ItemMenu_Role itemMenu_Role;
    public BaseInfo baseInfo;
    public Transform mainMenu;
    public ScreenFader screenFader;

    public static Controller_Main GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UnitManager.GetInstance().units.ForEach(u => u.GetComponent<Unit>().UnitClicked += OnUnitClicked);
        
        UnitSelected += baseInfo.UpdateView;
        ClearUI += baseInfo.Clear;
        ClearUI += skillMenu.Clear;
        ClearUI += itemMenu.Clear;
        ClearUI += itemMenu_Role.Clear;
        GameObject.Find("Canvas").transform.Find("MainMenu").gameObject.SetActive(true);
    }

    private void OnUnitClicked(object sender, EventArgs e)
    {
        if(!(Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) && !EventSystem.current.IsPointerOverGameObject())
        {
            character = (sender as Unit).transform;

            var outline = Camera.main.GetComponent<RenderBlurOutline>();
            if (outline)
                outline.RenderOutLine(character);

            if (UnitSelected != null)
                UnitSelected.Invoke(this, new EventArgs());
            mainMenu.gameObject.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var outline = Camera.main.GetComponent<RenderBlurOutline>();
            if (outline)
                outline.CancelRender();
            if (ClearUI != null)
                ClearUI.Invoke(this, new EventArgs());
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void NextScene()
    {
        screenFader.FadeOut(() => {
            Global.GetInstance().NextScene();
        }, true);
    }
}
