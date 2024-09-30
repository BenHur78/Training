#pragma once
#include "ProgressObserverWrapper.h"

namespace MeshingWrapper {

	//! \brief Options for I/O functionalities
	public ref class PcIOOptions
	{
	private:
		bool m_binary = true;
		bool m_hasNormals = true;
		bool m_hasColors = true;
		bool m_hasWeights = false;
		char m_asciiDecimalSeparator = '.';
		float m_scaleFactor = 1.f;
	};

	public ref class PcIOManager abstract sealed
	{
	public:
		//! \brief Load a point cloud file. Return false if it fails.
		//! \param path     The path to the file.   
		//! \param pc       The container point cloud.
		//! \param options          Set of parameters specifying which data should be loaded.
		//! \param progressObserver A handler of the progress notification and cancelation. If null, it is left unused.
		//! \returns        Bool that indicates if the loading was successful.
		static bool Load(System::String^ path, IProgressObserver^ progressObserver);		
	};
}

