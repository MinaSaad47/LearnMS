import { FaFacebook, FaGoogle, FaTwitter } from "react-icons/fa";

const Footer = () => {
  return (
    <footer className='relative'>
      <div className='absolute top-0 left-0 w-full overflow-hidden'>
        <svg
          data-name='Layer 1'
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 1200 120'
          preserveAspectRatio='none'>
          <path
            d='M321.39,56.44c58-10.79,114.16-30.13,172-41.86,82.39-16.72,168.19-17.73,250.45-.39C823.78,31,906.67,72,985.66,92.83c70.05,18.48,146.53,26.09,214.34,3V0H0V27.35A600.21,600.21,0,0,0,321.39,56.44Z'
            className='relative block fill-white'></path>
        </svg>
      </div>
      <div className='w-full text-white bg-blue-400 h-[300px] flex items-center justify-center gap-5'>
        <a className='transition-all duration-300 hover:cursor-pointer hover:scale-125'>
          <FaFacebook className='w-10 h-10' />
        </a>
        <a className='transition-all duration-300 hover:cursor-pointer hover:scale-125'>
          <FaGoogle className='w-10 h-10' />
        </a>
        <a className='transition-all duration-300 hover:cursor-pointer hover:scale-125'>
          <FaTwitter className='w-10 h-10' />
        </a>
      </div>
    </footer>
  );
};

export default Footer;