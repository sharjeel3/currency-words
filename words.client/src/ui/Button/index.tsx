import { FC } from "react";
import Style from "./index.module.scss";

interface InputProps {
  children: React.ReactNode;
  onClick?(): void;
}

const Button: FC<InputProps> = (props) => {
  const { children, onClick } = props;

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    event.preventDefault();
    if (onClick) onClick();
  };

  return (
    <>
      <button className={Style.button} onClick={handleClick}>
        {children}
      </button>
    </>
  );
};

export default Button;
