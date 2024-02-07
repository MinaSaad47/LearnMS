import { Flame, HandMetalIcon, School2 } from "lucide-react";
import { ReactNode } from "react";

const StudentHomePage = () => {
  return (
    <div className='w-full h-full'>
      <div
        className='flex flex-col items-center gap-4 pb-[9rem] pt-[4rem] rounded-b-full bg-cover'
        style={{
          backgroundImage:
            "url('https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2F4.bp.blogspot.com%2F-HfYl6MlH9gU%2FU0-VHPd0JeI%2FAAAAAAAAA5Q%2Fr0F4xE7a9v8%2Fs1600%2Fmath-photography-hd-wallpaper-1920x1200-9897.jpg&f=1&nofb=1&ipt=638a0886543efacbe870c72e4ea9f8825b66c8a0a4d15d32dea0c62fe381ca0d&ipo=images')",
        }}>
        <h1 className='text-6xl font-bold text-black'>MR RAFIK ISAAC</h1>
        <h2 className='text-2xl w-[60%] text-center font-bold text-black'>
          Don't Waste Your Time, Check Out Our Productive Courses. Our Self
          Improvement Courses Is Very Effective
        </h2>
        <button className='px-4 py-2 text-white transition-all duration-300 bg-blue-400 rounded-full hover:text-xl'>
          Let's Start
        </button>
        <div className='grid grid-cols-3 gap-4'>
          <CardItem
            title='Mission'
            description='We Foster Our Students’ Love For Learning, Encourage Them To Try New And Exciting Things, And Give Them A Solid Foundation To Build On.'
            icon={<School2 className='w-10 h-10' />}
          />
          <CardItem
            title='Vision'
            description='Our Vision Is To Develop Well Rounded, Confident And Responsible Individuals Who Aspire To Achieve Their Full Potential.'
            icon={<Flame className='w-10 h-10' />}
          />
          <CardItem
            title='Goal'
            description='If You Are Looking For High-Quality And Reliable Online Courses. It Will Be Us'
            icon={<HandMetalIcon className='w-10 h-10' />}
          />
        </div>
      </div>
    </div>
  );
};

export default StudentHomePage;

function CardItem({
  title,
  description,
  icon,
}: {
  title: string;
  description: string;
  icon: ReactNode;
}) {
  return (
    <div className='group hover:cursor-pointer w-[300px] flex flex-col items-center gap-2 h-[300px] bg-teal-200  text-blue-400 rounded-3xl p-4'>
      <div className='flex items-center justify-center w-20 h-20 text-white transition-all duration-300 bg-teal-900 rounded-full group-hover:bg-blue-400'>
        {icon}
      </div>
      <h3 className='text-2xl font-bold text-blue-400 transition-all duration-300 group-hover:text-teal-900'>
        {title}
      </h3>
      <p className='text-sm font-bold text-center text-black'>{description}</p>
    </div>
  );
}
