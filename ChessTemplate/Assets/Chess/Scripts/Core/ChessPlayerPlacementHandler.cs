using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {

        public delegate void OnSelectedValueChangeEventHandler(bool _isSelected);
        public event OnSelectedValueChangeEventHandler OnSelectedValueChange;


        [SerializeField] public int row, column;

        private bool _isSelected;

        private Vector3 _sizeAfterSelection = new Vector3(0.5f, 0.5f, 0f);

       

        public bool IsSelected { 
            get { return _isSelected; }
            set {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    OnSelectedValueChange?.Invoke(_isSelected);
                }
                
            }        
        }

      

        private void Start() {
            IsSelected = false;
            //transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }

        private void Update()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }

        void OnMouseDown()
        {
            if (IsSelected == false)
            {
                
                transform.localScale += _sizeAfterSelection;
                //Debug.Log($"{gameObject.name} ({row}, {column}) Selected.");
                IsSelected = true;
            }

            else
            {
                transform.localScale -= _sizeAfterSelection;
                //Debug.Log($"{gameObject.name} ({row}, {column}) Deselected.");
                IsSelected = false;
            }
            
        }

    }
}