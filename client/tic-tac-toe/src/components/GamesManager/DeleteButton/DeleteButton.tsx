import styled from 'styled-components';

const DeleteButton = styled.button`
    border: 2px solid var(--primary);
    background-color: #fff;
    font-size: 16px;
    height: 2em;
    width: 2em;
    border-radius: 999px;
    position: relative;
    font-size: 10px;
    cursor: pointer;

  &:after {
      content: "";
      display: block;
      background-color: grey;
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      height: 0.2em;
      width: 1em;
      background-color: var(--primary);
    }
  
  &:hover {
      background-color: var(--primary);
      border: 0;
    }
   &:hover:before, &:hover:after{
    background-color: var(--primary-light);
  }
`;

export default DeleteButton;