using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Списки
[RequireComponent( typeof( Canvas ) )]
public class GUIManager: MonoBehaviour
{
    public static event Action<WindowBase> OnWindowOpened
    {
        add { GUIElement<WindowBase>.OnOpened += value; }
        remove { GUIElement<WindowBase>.OnOpened -= value; }
    }
    public static event Action<WindowBase> OnWindowClosed
    {
        add { GUIElement<WindowBase>.OnClosed += value; }
        remove { GUIElement<WindowBase>.OnClosed -= value; }
    }

    public static event Action CloseAllWindowsEvent;

    public static GUIManager Instance { get; private set; }
    protected virtual void Awake()
    {
        if ( Instance == null )
        {
            Instance = this;
            StartCoroutine( Init() );
        }
    }

    protected virtual IEnumerator Init()
    {
        var canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        GUIElement<WindowBase>.InitElements( this );
        yield return new WaitForSeconds( 0.1f );
        canvas.enabled = true;
    }
	/*
    private void OnLoad( Player _player, bool _b )
    {
        if ( !_b && _player.IsCurrent() )
            CloseAllWindows( true );
    }
*/
    /// <summary>
    /// Возвращает окно. ВНИМАНИЕ: Не ищет по родительским классам, так что можно указать лишь класс конкретного окна
    /// </summary>
    public TWindowBase GetWindow<TWindowBase>() where TWindowBase: WindowBase
    {
        return GUIElement<WindowBase>.GetElement<TWindowBase>();
    }

    /// <summary>
    /// Возвращает окно. ВНИМАНИЕ: Не ищет по родительским классам, так что можно указать лишь класс конкретного окна
    /// </summary>
    public WindowBase GetWindow( Type _windowType )
    {
        return GUIElement<WindowBase>.GetElement( _windowType );
    }

    /// <summary>
    /// Показывает если одно данного класса открыто. Совместимо с полиморфией (можно указать родительский класс)
    /// </summary>
    public static bool IsWindowOpened<T>()
        where T: WindowBase
    {
        var list = GUIElement<WindowBase>.Opened;
        for ( int i = list.Count - 1; i >= 0; i-- )
        {
            if ( list[i] is T && list[i].IsOpened )
                return true;
        }
        return false;
    }

    /// <summary>
    /// Возвращает истину если окна открыты
    /// </summary>
    /// <param name="_any">Если истина, то возвращает true когда открыто любое из окно, иначе только если все</param>
    /// <param name="_windowTypes">Типы окон. Поддерживается полиморфизм.</param>
    /// <returns></returns>
    public static bool IsWindowOpened( bool _any, params Type[] _windowTypes )
    {
        var list = GUIElement<WindowBase>.Opened;
        byte cnt = 0;
        for ( int j = _windowTypes.Length - 1; j >= 0; j-- )
        {
            var type = _windowTypes[j];
            for ( int i = list.Count - 1; i >= 0; i-- )
            {
                if ( type.IsInstanceOfType( list[i] ) && list[i].IsOpened )
                {
                    if ( _any )
                        return true;
                    cnt++;
                    break;
                }
            }
        }
        return cnt == _windowTypes.Length;
    }

    /// <summary>
    /// Возвращает первое попавшееся открытое окно из списка. ПОддерживает полиморфизм
    /// </summary>
    public static WindowBase GetOpened( params Type[] _windowTypes )
    {
        var list = GUIElement<WindowBase>.Opened;
        for ( int j = 0; j < _windowTypes.Length; j++ )
        {
            var type = _windowTypes[j];
            for ( int i = list.Count - 1; i >= 0; i-- )
            {
                if ( type.IsInstanceOfType( list[i] ) && list[i].IsOpened )
                    return list[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Возвращает окно только если оно открыто
    /// </summary>
    public static T GetOpened<T>()
        where T: WindowBase
    {
        var wnd = Instance.GetWindow<T>();
        return wnd != null && wnd.IsOpened ? wnd : null;
    }

    public TWindowBase OpenWindow<TWindowBase>() where TWindowBase: WindowBase
    {
        TWindowBase window = GetWindow<TWindowBase>();
        if ( window != null )
            window.Open();
        return window;
    }
	/*
    public TWindowBase OpenWindow<TWindowBase>( EntityBase _entity ) where TWindowBase : WindowBase
    {
        TWindowBase window = GetWindow<TWindowBase>();
        if (window != null)
            window.Open(_entity);
        return window;
    }
*/
    private Queue<Action> windowOpen = new Queue<Action>();

    /// <summary>
    /// Вызывает указанное событие когда не открыто окон. Событие выставляется в очередь если другие окна уже ожидают открытия
    /// </summary>
    public void CallInQueue( Action _action )
    {
        if (!AnyWindowsOpened(false))
            _action.Execute();
        else
        {
            windowOpen.Enqueue( _action.Execute );
            OnWindowClosed -= OnCloseEvent;
            OnWindowClosed += OnCloseEvent;
        }
    }

    public TWindowBase OpenWindowQueued<TWindowBase>() where TWindowBase : WindowBase
    {
        TWindowBase wnd = GetWindow<TWindowBase>();
        if (!AnyWindowsOpened(false))
            wnd.Open();
        else
        {
            windowOpen.Enqueue(() => wnd.Open());
            OnWindowClosed -= OnCloseEvent;
            OnWindowClosed += OnCloseEvent;
        }
        return wnd;
    }
	/*
    public TWindowBase OpenWindowQueued<TWindowBase>(EntityBase _entity) where TWindowBase : WindowBase
    {
        TWindowBase wnd = GetWindow<TWindowBase>();
        if (!AnyWindowsOpened(false))
            wnd.Open(_entity);
        else
        {
            windowOpen.Enqueue(() => wnd.Open(_entity));
            OnWindowClosed -= OnCloseEvent;
            OnWindowClosed += OnCloseEvent;
        }
        return wnd;
    }
*/
    private Coroutine checkerRoutine;
    private void OnCloseEvent(WindowBase _windowBase)
    {
        if (checkerRoutine != null)
            StopCoroutine(checkerRoutine);
        checkerRoutine = StartCoroutine(CheckerRoutine());
    }

    IEnumerator CheckerRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        if (windowOpen.Count > 0)
            windowOpen.Dequeue()();

        if (windowOpen.Count == 0)
            OnWindowClosed -= OnCloseEvent;
        checkerRoutine = null;
    }

    public void CloseAllWindows( bool _forced = false )
    {
        if ( CloseAllWindowsEvent != null )
            CloseAllWindowsEvent();

        List<WindowBase> lWnd = GUIElement<WindowBase>.Opened;
        for ( int i = lWnd.Count - 1; i >= 0; i-- )
        {
            var wnd = lWnd[i];
            if ( wnd.IsOpened && ( _forced || !wnd.Settings.IsManualCloseOnly() ) )
                wnd.CloseAll();
        }
    }

    public bool AnyWindowsOpened( bool _ignorePopups = true )
    {
        List<WindowBase> lWnd = GUIElement<WindowBase>.Opened;
        for ( int i = lWnd.Count - 1; i >= 0; i-- )
        {
            WindowBase.WindowSettings sets = lWnd[i].Settings;
            if ((sets & WindowBase.WindowSettings.Secondary) > 0)
                continue;
            if (!_ignorePopups || (lWnd[i].Settings & WindowBase.WindowSettings.Popup) == 0)
                return true;
        }

        return false;
    }


#if UNITY_EDITOR || UNITY_ANDROID // Актуально только для Андроида

    void Update()
    {
        if ( !Input.anyKeyDown ) return;
        //if ( Input.GetKeyDown( KeyCode.Alpha1 ) ) OpenWindow<UIOrdersInterface>();
        //if ( Input.GetKeyDown( KeyCode.Alpha2 ) ) OpenWindow<UIPortOdersInterface>();
        if ( Input.GetKeyDown( KeyCode.Escape ) ) // Если нажали кнопку назад
        {
            bool wndOpened = AnyWindowsOpened(); // Были ли открыты какие-то окна
            CloseAllWindows(true); // закрываем всё
        //    if ( !wndOpened ) // Если ничего и не было открыто, то показываем банерообменник
          //          OpenWindow<QuitWindow>();
        }
    }

#endif
}
