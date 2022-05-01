import styled from 'styled-components'

const AddButton = styled.button`
  border: 2px solid var(--accent);
  background-color: #fff;
  font-size: 16px;
  height: 2.5em;
  width: 2.5em;
  border-radius: 999px;
  position: relative;
  font-size: 10px;
  cursor: pointer;

  &:after,
  &:before {
    content: '';
    display: block;
    background-color: grey;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  &:before {
    height: 1em;
    width: 0.2em;
    background-color: var(--accent);
  }

  &:after {
    height: 0.2em;
    width: 1em;
    background-color: var(--accent);
  }

  &:hover {
    background-color: var(--accent);
    border: 0;
  }
  &:hover:before,
  &:hover:after {
    background-color: var(--primary-light);
  }
`

export default AddButton
