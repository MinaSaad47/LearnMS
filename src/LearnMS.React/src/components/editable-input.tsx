import { cn } from "@/lib/utils";
import { LucideEdit2 } from "lucide-react";
import React from "react";
import { Button } from "./ui/button";
import { Input } from "./ui/input";

type EditableTextProps = React.ComponentProps<typeof Input>;

const EditableInput = ({ className, ...props }: EditableTextProps) => {
  const [isEditing, setIsEditing] = React.useState(false);
  const [insideInput, setInsideInput] = React.useState(false);

  return (
    <div
      className={cn("relative flex items-center", className)}
      onMouseEnter={() => setInsideInput(true)}
      onMouseLeave={() => setInsideInput(false)}>
      <Input
        {...props}
        disabled={!isEditing}
        className={`border-none focus:outline-none ring-stone-950 disabled:cursor-default ${
          isEditing && "outline outline-1 outline-neutral-700"
        }`}
      />
      {insideInput && (
        <Button
          size='icon'
          className='absolute top-0 left-0 w-6 h-6 -translate-x-1/2 -translate-y-1/2'
          variant='secondary'
          onClick={() => {
            setIsEditing(!isEditing);
          }}>
          <LucideEdit2 className='w-3 h-3' />
        </Button>
      )}
    </div>
  );
};

export default EditableInput;
