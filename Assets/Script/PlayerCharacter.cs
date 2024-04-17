using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int collisionCount = 0; // Compteur de collisions
    
    public int dimensionALayer;
    public int dimensionBLayer;
    public enum Dimension { DimensionA, DimensionB }
    public Dimension currentDimension = Dimension.DimensionA;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            collisionCount++; // Incrémenter le compteur de collisions
            Debug.Log("Collision detected! Total hits: " + collisionCount);
        }
    }
    
    public void SwitchDimension() 
    {
        if (currentDimension == Dimension.DimensionA) 
        {
            currentDimension = Dimension.DimensionB;
            // Logique pour switcher à la Dimension B
            SwitchToDimensionB();
        } else 
        {
            currentDimension = Dimension.DimensionA;
            // Logique pour switcher à la Dimension A
            SwitchToDimensionA();
        }
    }
    
    // Méthode pour passer à la dimension A
    public void SwitchToDimensionA()
    {
        gameObject.layer = dimensionALayer;  // Change le layer du joueur
        // Physics.IgnoreLayerCollision(dimensionALayer, dimensionBLayer, true); // Ignore les collisions avec les obstacles de Dimension B
        // Physics.IgnoreLayerCollision(dimensionBLayer, dimensionALayer, false); // Optionnelle, car elle ne devrait pas être nécessaire
        Debug.Log("Switched to Dimension A");
    }
    
    // Méthode pour passer à la dimension B
    public void SwitchToDimensionB()
    {
        gameObject.layer = dimensionBLayer;  // Change le layer du joueur
        // Physics.IgnoreLayerCollision(dimensionALayer, dimensionBLayer, false); // Optionnelle, car elle ne devrait pas être nécessaire
        // Physics.IgnoreLayerCollision(dimensionBLayer, dimensionALayer, true); // Ignore les collisions avec les obstacles de Dimension A
        Debug.Log("Switched to Dimension B");
    }
}
