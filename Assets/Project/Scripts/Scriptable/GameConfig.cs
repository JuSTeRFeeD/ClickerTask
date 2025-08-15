using System.Linq;
using UnityEngine;

namespace Project.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private BusinessConfig[] _businesses;
        
        public BusinessConfig[] Businesses => _businesses;

        public BusinessConfig GetBusinessById(int id)
        {
            return _businesses.FirstOrDefault(x => x.Id == id);
        }
    }
}