import { FC } from "react";
import Style from "./index.module.scss";

interface InputProps {
  label: string;
  value: string;
  onChange(value: string): void;
  required?: boolean;
}

const Input: FC<InputProps> = (props) => {
  const { label, value, required, onChange } = props;

  return (
    <div className={Style.root}>
      <label className={Style.label}>{label}</label>
      <input
        className={Style.input}
        required={required}
        type="number"
        value={value}
        onChange={(event) => onChange(event.target.value)}
      />
    </div>
  );
};

export default Input;
