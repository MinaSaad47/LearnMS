import { useAssistantsQuery } from "@/api/assistants-api";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import {
  HoverCard,
  HoverCardContent,
  HoverCardTrigger,
} from "@/components/ui/hover-card";
import { useModalStore } from "@/store/use-modal-store";
import { Assistant } from "@/types/assistants";
import { Edit2, Shield } from "lucide-react";

const AssistantsPage = () => {
  const { data: assistants, isLoading } = useAssistantsQuery();
  const { openModal } = useModalStore();

  if (isLoading) {
    return (
      <div className='flex items-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  return (
    <div className='flex flex-col w-full h-full gap-2 p-2'>
      <Button
        className='self-end'
        onClick={() => openModal("add-assistant-modal")}>
        Add Assistant
      </Button>
      <AssistantsList assistants={assistants?.data!.items!} />
    </div>
  );
};

function AssistantsList({ assistants }: { assistants: Assistant[] }) {
  return (
    <div className='flex flex-col gap-2 p-4 '>
      {assistants?.map((assistant) => (
        <AssistantListItem key={assistant.id} assistant={assistant} />
      ))}
    </div>
  );
}

function AssistantListItem({ assistant }: { assistant: Assistant }) {
  const { openModal } = useModalStore();

  return (
    <div className='p-2 border-[3px] border-blue-400 rounded flex justify-between items-center'>
      <div className='flex items-center justify-center gap-2'>
        <div className='flex items-center justify-center w-10 h-10 text-white bg-blue-400 rounded-full'>
          <Shield />
        </div>
        <div className='text-xl'>{assistant.email}</div>
      </div>
      <div className='flex items-center'>
        <HoverCard>
          <HoverCardTrigger asChild>
            <Button variant='link'>@permissions</Button>
          </HoverCardTrigger>
          <HoverCardContent className='w-fit'>
            {assistant.permissions?.map((permission) => (
              <div className='text-sm text-blue-600' key={permission}>
                {permission}
              </div>
            ))}
          </HoverCardContent>
        </HoverCard>
        <Button
          size='icon'
          variant={"link"}
          onClick={() => openModal("update-assistant-modal", { assistant })}>
          <Edit2 />
        </Button>
      </div>
    </div>
  );
}

export default AssistantsPage;
