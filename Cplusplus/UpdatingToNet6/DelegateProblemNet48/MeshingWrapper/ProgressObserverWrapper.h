#pragma once
namespace MeshingWrapper {

#pragma managed(push, off)
	//! \brief Native progress observer. In this case, it takes pointers to c# functions and executes them.
	class NativeProgressObserver
	{
	public:
		using CancelledFun = bool(*)();
		using NotifyFun = void(*)(double);

	public:
		//! \brief Construct a native progress observer, with pointers to functions.
		//! This is needed since we want, in this case, use functions coming from c#
		//! \param c The pointer to the c# function managing the canceled state.
		//! \param f The pointer to the c# function managing the notify operations.
		NativeProgressObserver(CancelledFun c, NotifyFun f);

		//! \brief Execute the stored CancelledFun.
		//! \returns True if the operation has been canceled.
		bool canceled() const;

		//! \brief Execute the stored NotifyFun.
		//! \param percentage The current progress percentage.
		void notifyProgress(double percentage);

	private:
		//! \brief This is the magic: instead of storing members (m_canceled, m_percentage,...),
		//! we store pointers to C# functions, in order to directly interact with c# app.

		//! \brief Store the pointer to the c# function managing the canceled state.
		CancelledFun m_canceled = nullptr;

		//! \brief Store the pointer to the c# function managing the notify operations.
		NotifyFun m_notify = nullptr;
	};
#pragma managed(pop)

	//! \brief Interface for creating any type of progress
	public interface class IProgressObserver
	{
	public:
		//! \brief Update the observer with the input percentage.
		//! \param perdentage The current progress percentage. 
		void Notify(double percentage);
		//! \brief Check if the operation has been canceled.
		//! \returns True if the user manually cancelled the operation.
		bool Canceled();
	};

	//! \brief Wrapper around a native progress observer.
	//! In costructions, creates pointers to the c# Notify and Canceled
	//! functions and passes them to the native observer, in order to
	//! execute c# methods during c++ routines.
	public ref class ProgressObserverWrapper
	{
		//! \brief Definition of the needed delegate.
		//! These will be converted to pointers and passed to the native progress observer.
		delegate bool CancelledDelegate();
		delegate void NotifyDelegate(double perc);

	public:
		//! \brief Construct the wrapper starting from an interface.
		//! Convert the c# functions Canceled and Notify to functions pointers.
		//! Create a pointer to a native progress observer using the functions pointers created.
		ProgressObserverWrapper(IProgressObserver^ prg);
		
		//! \brief Call the !ProgressObserverWrapper method
		~ProgressObserverWrapper();

		//! \brief  Delete the pointer to the native progress.
		!ProgressObserverWrapper();

	internal:
		//! \brief Get the pointer to the native progress observer.
		//! \returns The pointer to the stored native progress.
		NativeProgressObserver* nativeProgress();

	private:
		//! \brief The stored unmanaged c++ native progress observer.
		NativeProgressObserver* m_nativeProgress;

		//! \brief C# managed progress observer.
		IProgressObserver^ m_progressObserver;

		//! \brief The delegates bound to the m_progressObserver object.
		CancelledDelegate^ m_cancelDelegate;
		NotifyDelegate^ m_notifyDelegate;
	};
}

