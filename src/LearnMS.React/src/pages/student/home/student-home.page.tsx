import { useProfileQuery } from "@/api/profile-api";
import Footer from "@/components/footer";
import { Flame, HandMetalIcon, School2 } from "lucide-react";
import { ReactNode } from "react";
import { Link } from "react-router-dom";

const StudentHomePage = () => {
  return (
    <div className='flex flex-col w-full min-h-full'>
      <HomeSection />
      <div className='mt-auto'>
        <Footer />
      </div>
    </div>
  );
};

const HomeSection = () => {
  const { profile } = useProfileQuery();
  return (
    <section className='font-patua min-h-[1400px] md:min-h-[800px] flex flex-col items-center text-center p-10'>
      <div className="z-10 absolute top-[250px] md:top-[140px]  md:right-[70px] md:w-[500px] w-[400px] h-[400px] md:h-[500px] bg-cover bg-center bg-repeat overflow-hidden bg-[url('/home.png')]">
        <svg
          className='absolute left-0 z-10 -bottom-[100px] md:bottom-0 h-72'
          data-name='Layer 1'
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 1200 120'
          preserveAspectRatio='none'>
          <path
            d='M985.66,92.83C906.67,72,823.78,31,743.84,14.19c-82.26-17.34-168.06-16.33-250.45.39-57.84,11.73-114,31.07-172,41.86A600.21,600.21,0,0,1,0,27.35V120H1200V95.8C1132.19,118.92,1055.71,111.31,985.66,92.83Z'
            className=''
            fill='white'></path>
        </svg>
      </div>
      <div className='z-20 flex flex-col items-start gap-10 m-auto my-0 md:my-10'>
        <h1 className='text-4xl font-bold md:text-6xl text-color1'>
          MR RAFIK ISAAC
        </h1>
        <h2 className='text-3xl font-bold text-color1'>Newton's Academy</h2>

        {!profile?.isAuthenticated && (
          <Link
            to='/sign-in-sign-up'
            className='self-start px-4 py-2 text-xl text-white transition-all duration-300 rounded-full md:mr-0 bg-color2 w-fit hover:text-xl'>
            <button>Let's Start</button>
          </Link>
        )}
        <div className='flex flex-wrap items-center justify-center gap-4 mt-72 md:mt-20'>
          <CardItem
            title='Learn'
            description=''
            icon={<School2 className='w-10 h-10' />}
          />
          <CardItem
            title='Practice'
            description=''
            icon={<Flame className='w-10 h-10' />}
          />
          <CardItem
            title='Achieve'
            description=''
            icon={<HandMetalIcon className='w-10 h-10' />}
          />
        </div>
      </div>
    </section>
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
    <div className='group hover:cursor-pointer shadow-color2 w-[300px]  flex justify-center flex-col items-center gap-2 h-[300px] bg-white/60 border-[rgb(96, 112, 255, 0.7)] border-[3px] shadow-2xl text-color2 rounded-3xl p-4 '>
      <div className='flex items-center justify-center w-20 h-20 text-white transition-all duration-300 rounded-full bg-color2 group-hover:bg-color1'>
        {icon}
      </div>
      <h3 className='text-2xl font-bold transition-all duration-300 text-color2 group-hover:text-color1'>
        {title}
      </h3>
      {description && (
        <p className='text-sm font-bold text-center text-color2'>
          {description}
        </p>
      )}
    </div>
  );
}
