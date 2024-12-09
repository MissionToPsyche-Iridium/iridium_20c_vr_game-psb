using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.InputSystem;
public class PauseMenuManagerTests
{
    private GameObject menuObject;
    private GameObject headObject;
    private PauseMenuManager pauseMenuManager;
    private InputAction showButtonAction;
    private InputActionProperty showButtonProperty;

    [SetUp]
    public void Setup()
    {
        // Create the head GameObject with a Transform
        headObject = new GameObject("Head");
        headObject.transform.position = Vector3.zero;

        // Create the menu GameObject
        menuObject = new GameObject("Menu");
        menuObject.SetActive(false); // Menu starts inactive

        // Create the PauseMenuManager and attach the head and menu objects
        pauseMenuManager = menuObject.AddComponent<PauseMenuManager>();
        pauseMenuManager.head = headObject.transform;
        pauseMenuManager.menu = menuObject;

        // Create the InputAction for the showButton (simulate VR left controller menu button)
        showButtonAction = new InputAction(binding: "<XRController>{LeftHand}/menu");
        showButtonAction.Enable();

        // Create the InputActionProperty and assign the InputAction here (this is not directly assignable in the test)
        showButtonProperty = new InputActionProperty();
        // Here we initialize the action of the property using a method instead of direct assignment.
        showButtonProperty = new InputActionProperty { action = showButtonAction }; // **This still triggers an error!**

        // Now assign the InputActionProperty to the pause menu manager
        pauseMenuManager.showButton = showButtonProperty;
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup after tests
        Object.Destroy(menuObject);
        Object.Destroy(headObject);
        showButtonAction.Disable();
    }

    [UnityTest]
    public IEnumerator TestMenuToggleOnButtonPress()
    {
        // Initially, the menu should not be active
        Assert.IsFalse(menuObject.activeSelf);

        // Simulate the button press
        showButtonAction.ReadValue<float>(); // This triggers the action, as we assume the action is linked to the button press

        // Wait for the next frame so the menu update can process
        yield return null;

        // Assert that the menu is now active
        Assert.IsTrue(menuObject.activeSelf);

        // Assert that Time.timeScale is 0.0f when the menu is active
        Assert.AreEqual(0.0f, Time.timeScale);

        // Simulate pressing the button again to close the menu
        showButtonAction.ReadValue<float>(); // Simulate another press

        // Wait for the next frame so the menu update can process
        yield return null;

        // Assert that the menu is inactive
        Assert.IsFalse(menuObject.activeSelf);

        // Assert that Time.timeScale is reset to 1.0f when the menu is inactive
        Assert.AreEqual(1.0f, Time.timeScale);
    }
}