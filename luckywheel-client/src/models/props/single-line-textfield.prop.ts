import type {InputTypes} from "@/models/input-types.ts";

export interface SingleLineTextfieldProp {
  maxLength?: number;
  disabled?: boolean;
  alt?: string;
  placeholder?: string;
  labelId?: string;
  inputType: InputTypes
}
